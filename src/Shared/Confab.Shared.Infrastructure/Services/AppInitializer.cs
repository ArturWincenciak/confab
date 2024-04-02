﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Confab.Shared.Infrastructure.Services;

internal class AppInitializer : IHostedService
{
    private readonly ILogger<AppInitializer> _logger;
    private readonly IServiceProvider _serviceProvider;

    public AppInitializer(IServiceProvider serviceProvider, ILogger<AppInitializer> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Hosted service named AppInitializer starting StartAsync method ...");

        var dbContextTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => typeof(DbContext).IsAssignableFrom(x) && !x.IsInterface && x != typeof(DbContext));

        using var scope = _serviceProvider.CreateScope();
        foreach (var dbContextType in dbContextTypes)
        {
            var dbContext = scope.ServiceProvider.GetService(dbContextType) as DbContext;
            if (dbContext is null)
            {
                _logger.LogInformation($"DB context '{dbContextType}' doesn't exist. " +
                                       "Probably module with the DB context is switched off " +
                                       "so then do nothing with them.");
                continue;
            }

            _logger.LogInformation($"Migrating of '{dbContextType.FullName}' DB context ...");
            await dbContext.Database.MigrateAsync(cancellationToken);
            _logger.LogInformation($"Migration of '{dbContextType.FullName}' has been completed.");
        }

        _logger.LogInformation("Has been called all StartAsync method of AppInitializer hosted service.");
    }

    public Task StopAsync(CancellationToken cancellationToken) =>
        Task.CompletedTask;
}