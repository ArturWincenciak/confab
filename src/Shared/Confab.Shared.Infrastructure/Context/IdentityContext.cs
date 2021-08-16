﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Confab.Shared.Abstractions.Contexts;
using Microsoft.IdentityModel.Tokens;

namespace Confab.Shared.Infrastructure.Context
{
    internal class IdentityContext : IIdentityContext
    {
        public IdentityContext(ClaimsPrincipal principal)
        {
            IsAuthenticated = principal.Identity?.IsAuthenticated is true;
            Id = IsAuthenticated ? Guid.Parse(principal.Identity.Name) : Guid.Empty;
            Role = principal.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
            Claims = principal.Claims.GroupBy(x => x.Type)
                .ToDictionary(x => x.Key, x => x.Select(c => c.Value.ToString()));
        }

        public bool IsAuthenticated { get; }
        public Guid Id { get; }
        public string Role { get; }
        public Dictionary<string, IEnumerable<string>> Claims { get; }
    }
}