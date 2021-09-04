using System;
using System.Text;
using System.Text.Json;

namespace Confab.Shared.Infrastructure.Modules
{
    internal class JsonModuleSerializer : IModuleSerializer
    {
        private static readonly JsonSerializerOptions SerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public byte[] Serialize<T>(T value)
        {
            return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value, SerializerOptions));
        }

        public T Deserialize<T>(byte[] value)
        {
            return JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(value), SerializerOptions);
        }

        public object Deserialize(byte[] value, Type type)
        {
            return JsonSerializer.Deserialize(Encoding.UTF8.GetString(value), type, SerializerOptions);
        }
    }
}