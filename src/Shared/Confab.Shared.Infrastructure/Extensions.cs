﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Confab.Shared.Abstractions;
using Confab.Shared.Abstractions.Modules;
using Confab.Shared.Abstractions.Storage;
using Confab.Shared.Infrastructure.Api;
using Confab.Shared.Infrastructure.Auth;
using Confab.Shared.Infrastructure.Commands;
using Confab.Shared.Infrastructure.Contexts;
using Confab.Shared.Infrastructure.Events;
using Confab.Shared.Infrastructure.Exceptions;
using Confab.Shared.Infrastructure.Kernel;
using Confab.Shared.Infrastructure.Messaging;
using Confab.Shared.Infrastructure.Modules;
using Confab.Shared.Infrastructure.Postgres;
using Confab.Shared.Infrastructure.Queries;
using Confab.Shared.Infrastructure.Services;
using Confab.Shared.Infrastructure.Storage;
using Confab.Shared.Infrastructure.Time;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

[assembly: InternalsVisibleTo("Confab.Bootstrapper")]

namespace Confab.Shared.Infrastructure
{
    internal static class Extensions
    {
        private const string CorsPolicy = "cors";

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IList<IModule> modules,
            IEnumerable<Assembly> assemblies)
        {
            var disabledModules = new List<string>();
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();

                Console.WriteLine("Recursive config printing...");
                PrintConfiguration(configuration.GetChildren());

                Console.WriteLine("\n\nConfiguration:");
                foreach (var (key, value) in configuration.AsEnumerable())
                {
                    Console.WriteLine($"Key: '{key}', Value: '{value}'");
                    if (!key.Contains(":module:enabled"))
                        continue;

                    if (!bool.Parse(value))
                    {
                        var splitKey = key.Split(":");
                        var moduleName = splitKey[0];
                        disabledModules.Add(moduleName);
                        Console.WriteLine($"---\nDisabled module '{moduleName}'" +
                                          $"by key '{key}' with value '{value}'\n---");
                    }
                }
            }

            services.AddCors(corsOption =>
            {
                corsOption.AddPolicy(CorsPolicy, builder =>
                {
                    builder
                        .WithOrigins("*")
                        .WithMethods("POST", "PUT", "DELETE")
                        .WithHeaders("Content-Type", "Authorization");
                });
            });
            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => type.FullName);
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Confab API",
                    Version = "v1"
                });
            });

            services.AddMemoryCache();
            services.AddSingleton<IRequestStorage, RequestStorage>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IContextFactory, ContextFactory>();
            services.AddTransient(sp => sp.GetRequiredService<IContextFactory>().Create());
            services.AddModuleInfo(modules);
            services.AddModuleRequests(assemblies);
            services.AddAuth(modules);
            services.AddErrorHandling();
            services.AddCommands(assemblies);
            services.AddMessaging();
            services.AddEvents(assemblies);
            services.AddDomainEvents(assemblies);
            services.AddQueries(assemblies);
            services.AddPostgres();
            services.AddSingleton<IClock, UtcClock>();
            services.AddHostedService<AppInitializer>();

            services.AddControllers()
                .ConfigureApplicationPartManager(manager =>
                {
                    Console.WriteLine("\nConfigure application part manager starting with parts: " +
                                      $"[{string.Join(" | ", manager.ApplicationParts.Select(x => x.Name))}].");

                    var removedParts = new List<ApplicationPart>();
                    foreach (var disabledModule in disabledModules)
                    {
                        var parts = manager.ApplicationParts
                            .Where(applicationPart => applicationPart.Name.Contains(disabledModule,
                                StringComparison.InvariantCultureIgnoreCase));

                        removedParts.AddRange(parts);
                    }

                    foreach (var part in removedParts)
                        manager.ApplicationParts.Remove(part);

                    Console.WriteLine("Removed application parts: " +
                                      $"[{string.Join(" | ", removedParts.Select(x => x.Name))}].");

                    manager.FeatureProviders.Add(new InternalControllerFeatureProvider());

                    Console.WriteLine("Configure application part manager done.");
                });

            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseCors(CorsPolicy);
            app.UseErrorHandling();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Confab Api"));
            app.UseReDoc(options =>
            {
                options.RoutePrefix = "docs";
                options.SpecUrl("/swagger/v1/swagger.json");
                options.DocumentTitle = "Confab API";
            });

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            return app;
        }

        public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
        {
            Console.WriteLine(
                $"Build Service Provider by call GetOption of '{typeof(T)}' type to get option '{sectionName}' name.");

            using var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetService<IConfiguration>();
            return configuration.GetOptions<T>(sectionName);
        }

        private static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
        {
            var options = new T();
            configuration.GetSection(sectionName).Bind(options);
            return options;
        }

        private static void PrintConfiguration(IEnumerable<IConfigurationSection> configurationSections)
        {
            var children = configurationSections.ToArray();
            foreach (var child in children)
                if (child is not null)
                {
                    var key = child.Key;
                    var value = child.Value;
                    var path = child.Path;
                    Console.WriteLine($"Path: [{path}], Key: [{key}], Value: [{value}]");

                    var childChildren = child.GetChildren().ToArray();
                    if (childChildren.Any())
                        PrintConfiguration(childChildren);
                }
        }
    }
}