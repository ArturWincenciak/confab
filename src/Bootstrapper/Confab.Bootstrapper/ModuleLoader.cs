using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Confab.Shared.Abstractions.Modules;
using Microsoft.Extensions.Configuration;

[assembly: InternalsVisibleTo("Confab.Tests.Integrations")]

namespace Confab.Bootstrapper
{
    internal static class ModuleLoader
    {
        public static IList<Assembly> LoadAssemblies(IConfiguration configuration)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var loadedLocations = assemblies
                .Where(x => !x.IsDynamic)
                .Select(x => x.Location).
                ToList();

            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var files = Directory.GetFiles(baseDir, "*.dll").ToList();
            files = files
                .Where(file => !loadedLocations.Contains(file, StringComparer.InvariantCultureIgnoreCase))
                .ToList();

            FilterFilesToLoadBasedOnConfiguration(files, configuration);

            files.ForEach(file =>
            {
                var assemblyName = AssemblyName.GetAssemblyName(file);
                var assembly = AppDomain.CurrentDomain.Load(assemblyName);
                assemblies.Add(assembly);
            });

            return assemblies;
        }

        public static IList<IModule> LoadModules(IEnumerable<Assembly> assemblies)
        {
            var modules = assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => typeof(IModule).IsAssignableFrom(type) && !type.IsInterface)
                .OrderBy(type => type.Name)
                .Select(Activator.CreateInstance)
                .Cast<IModule>()
                .ToList();

            return modules;
        }

        private static void FilterFilesToLoadBasedOnConfiguration(List<string> files, IConfiguration configuration)
        {
            const string modulePart = "Confab.Modules.";
            var disabledModules = new List<string>();
            foreach (var file in files)
            {
                if (!file.Contains(modulePart))
                    continue;

                var splitByModulePart = file.Split(modulePart);
                var secondPartContainsModuleName = splitByModulePart[1];
                var splitByDot = secondPartContainsModuleName.Split(".");
                var moduleName = splitByDot[0];
                moduleName = moduleName.ToLowerInvariant();
                var enabled = configuration.GetValue<bool>($"{moduleName}:module:enabled");
                if (!enabled)
                    disabledModules.Add(file);
            }

            foreach (var disabledModule in disabledModules)
            {
                files.Remove(disabledModule);
            }
        }
    }
}