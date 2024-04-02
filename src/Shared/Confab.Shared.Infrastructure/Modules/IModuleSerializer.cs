using System;

namespace Confab.Shared.Infrastructure.Modules;

internal interface IModuleSerializer
{
    byte[] Serialize<T>(T value);
    T Deserialize<T>(byte[] value);
    object Deserialize(byte[] value, Type type);
}