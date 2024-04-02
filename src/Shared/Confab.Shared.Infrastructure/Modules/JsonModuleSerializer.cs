﻿using System;
using System.Text;
using System.Text.Json;

namespace Confab.Shared.Infrastructure.Modules;

internal class JsonModuleSerializer : IModuleSerializer
{
    private readonly static JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public byte[] Serialize<T>(T value) =>
        Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value, SerializerOptions));

    public T Deserialize<T>(byte[] value) =>
        JsonSerializer.Deserialize<T>(json: Encoding.UTF8.GetString(value), SerializerOptions);

    public object Deserialize(byte[] value, Type type) =>
        JsonSerializer.Deserialize(json: Encoding.UTF8.GetString(value), type, SerializerOptions);
}