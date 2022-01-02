using System;
using System.Collections.Generic;
using Confab.Shared.Abstractions.Commands;

namespace Confab.Shared.Infrastructure.Postgres
{
    internal class PostgresUnitOfWorkTypeRegistry
    {
        private readonly Dictionary<string, Type> _types = new();

        public void Register<T>() where T : class, IUnitOfWork
        {
            var key = GetKey<T>();
            if (string.IsNullOrWhiteSpace(key))
                throw new InvalidOperationException($"Cannot resolve module key for type {typeof(T).FullName}.");

            _types[key] = typeof(T);
        }

        public Type Resolve<T>() where T : class, ICommand
        {
            return _types.TryGetValue(GetKey<T>(), out var type) ? type : null;
        }

        private static string GetKey<T>()
        {
            return typeof(T).GetModuleName();
        }
    }
}