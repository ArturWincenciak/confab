using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Confab.Modules.Users.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confab.Modules.Users.Core.DAL.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    private readonly static JsonSerializerOptions SerializerOption = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(x => x.Email).IsUnique();
        builder.Property(x => x.Password).IsRequired();
        builder.Property(x => x.Role).IsRequired();
        builder.Property(x => x.Claims)
            .HasConversion(
                convertToProviderExpression: x => JsonSerializer.Serialize(x, SerializerOption),
                convertFromProviderExpression: x =>
                    JsonSerializer.Deserialize<Dictionary<string, IEnumerable<string>>>(x, SerializerOption));
        builder.Property(x => x.Claims).Metadata.SetValueComparer(
            new ValueComparer<Dictionary<string, IEnumerable<string>>>(
                equalsExpression: (c1, c2) => c1.SequenceEqual(c2),
                hashCodeExpression: c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                snapshotExpression: c => c.ToDictionary(x => x.Key, x => x.Value)));
    }
}