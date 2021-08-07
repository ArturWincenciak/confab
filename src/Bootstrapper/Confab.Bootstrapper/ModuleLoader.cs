﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Confab.Shared.Abstractions.Modules;
using Microsoft.Extensions.Configuration;

namespace Confab.Bootstrapper
{
    internal static class ModuleLoader
    {
        public static IList<Assembly> LoadAssemblies(IConfiguration configuration)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

            int logInc = 1;
            Console.WriteLine("\n\nCurrent app domain assemblies:");
            assemblies.ForEach(x =>
            {
                Console.WriteLine($"*[{logInc++}] [{x.ImageRuntimeVersion}] [{x.Location}] [{x.IsDynamic}]");
            });

            var loadedLocations = assemblies.Where(x => !x.IsDynamic).Select(x => x.Location).ToList();

            logInc = 1;
            Console.WriteLine("\n\nFiltered - all not dynamic assemblies' locations:");
            loadedLocations.ForEach(x => Console.WriteLine($"*[{logInc++}] [{x}]"));

            var baseDir = AppDomain.CurrentDomain.BaseDirectory;

            Console.WriteLine($"\n\nCurrent app domain base directory: [{baseDir}].");

            var files = Directory.GetFiles(baseDir, "*.dll").ToList();

            logInc = 1;
            Console.WriteLine("\n\nAll dll files in current app domain base directory:");
            files.ForEach(file => Console.WriteLine($"*[{logInc++}] [{file}]"));

            files = files
                .Where(file => !loadedLocations.Contains(file, StringComparer.InvariantCultureIgnoreCase))
                .ToList();

            logInc = 1;
            Console.WriteLine("\n\nAll dll files in current app domain base directory that not loaded yet:");
            files.ForEach(file => Console.WriteLine($"*[{logInc++}] [{file}]"));

            Console.WriteLine("\n\nSwitching on/off module by configuration ...");
            const string modulePart = "Confab.Modules.";
            var disabledModules = new List<string>();
            foreach (var file in files)
            {
                if (!file.Contains(modulePart))
                {
                    continue;
                }

                Console.WriteLine($"Working on '{file}' to check configuration enabled flag.");
                var splitByModulePart = file.Split(modulePart);
                var secondPartContainsModuleName = splitByModulePart[1];
                var splitByDot = secondPartContainsModuleName.Split(".");
                var moduleName = splitByDot[0];
                moduleName = moduleName.ToLowerInvariant();
                var enabled = configuration.GetValue<bool>($"{moduleName}:module:enabled");
                Console.WriteLine($"Configuration enabled property of '{moduleName}' module has value: '{enabled}'.");
                if (!enabled)
                {
                    disabledModules.Add(file);
                }
            }

            foreach (var disabledModule in disabledModules)
            {
                Console.WriteLine($"Module '{disabledModule}' is disabled.");
                files.Remove(disabledModule);
            }

            logInc = 1;
            Console.WriteLine("\n\nFinally the new dll files that will be loaded:");
            files.ForEach(file => Console.WriteLine($"*[{logInc++}] [{file}]"));

            files.ForEach(file =>
            {
                AssemblyName assemblyName = AssemblyName.GetAssemblyName(file);
                Assembly assembly = AppDomain.CurrentDomain.Load(assemblyName);
                assemblies.Add(assembly);
            });

            logInc = 1;
            Console.WriteLine("\n\nAll app domain assemblies dll files loaded: ");
            assemblies.ForEach(x =>
            {
                Console.WriteLine($"*[{logInc++}] [{x.ImageRuntimeVersion}] [{x.Location}] [{x.IsDynamic}]");
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

            if (modules.Any())
            {
                Console.WriteLine("\n***\nModules enabled and loaded:");
                modules.ForEach(module => Console.WriteLine($"\t* [{module.Name}]"));
                Console.WriteLine("\n***\n");
            }
            else
            {
                Console.WriteLine("\n***\nThere is no one enabled module.\n***\n");
            }

            return modules;
        }
    }
}