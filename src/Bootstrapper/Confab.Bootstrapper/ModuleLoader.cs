using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Confab.Shared.Abstractions.Modules;

namespace Confab.Bootstrapper
{
    internal static class ModuleLoader
    {
        public static IList<Assembly> LoadAssemblies()
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

            files.ForEach(file =>
            {
                AssemblyName assemblyName = AssemblyName.GetAssemblyName(file);
                Assembly assembly = AppDomain.CurrentDomain.Load(assemblyName);
                assemblies.Add(assembly);
            });

            logInc = 1;
            Console.WriteLine(
                "\n\nApp domain assemblies after loaded dll files from current app domain base directory:");
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

            Console.WriteLine("\nModules:");
            modules.ForEach(module => Console.WriteLine($"\t* [{module.Name}]"));
            Console.WriteLine("\n");

            return modules;
        }
    }
}
