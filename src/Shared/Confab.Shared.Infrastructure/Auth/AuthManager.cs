using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Confab.Shared.Abstractions;
using Confab.Shared.Abstractions.Auth;
using Microsoft.IdentityModel.Tokens;

namespace Confab.Shared.Infrastructure.Auth;

public sealed class AuthManager : IAuthManager
{
    private readonly static Dictionary<string, IEnumerable<string>> EmptyClaims = new();
    private readonly IClock _clock;
    private readonly string _issuer;
    private readonly AuthOptions _options;
    private readonly SigningCredentials _signingCredentials;

    public AuthManager(AuthOptions options, IClock clock)
    {
        if (options?.IssuerSigningKey is null) throw new InvalidOperationException("Issuer signing key not set.");

        _options = options;
        _clock = clock;
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.IssuerSigningKey));
        _signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        _issuer = options.Issuer;
    }

    public JsonWebToken CreateToken(string userId, string role = null, string audience = null,
        IDictionary<string, IEnumerable<string>> claims = null)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException(message: "User ID claim (subject) cannot be empty.", paramName: nameof(userId));

        var now = _clock.CurrentDate();
        var jwtClaims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId),
            new(JwtRegisteredClaimNames.UniqueName, userId),
            new(JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, value: new DateTimeOffset(now).ToUnixTimeMilliseconds().ToString())
        };

        if (!string.IsNullOrWhiteSpace(role))
            jwtClaims.Add(new(ClaimTypes.Role, role));

        if (!string.IsNullOrWhiteSpace(audience))
            jwtClaims.Add(new(JwtRegisteredClaimNames.Aud, audience));

        if (claims?.Any() is true)
        {
            var customClaims = new List<Claim>();
            foreach (var (claim, values) in claims)
                customClaims.AddRange(values.Select(value => new Claim(claim, value)));

            jwtClaims.AddRange(customClaims);
        }

        var expires = now.Add(_options.Expiry);

        var jwt = new JwtSecurityToken(_issuer, claims: jwtClaims, notBefore: now, expires: expires,
            signingCredentials: _signingCredentials);

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return new()
        {
            AccessToken = token,
            RefreshToken = string.Empty,
            Expires = new DateTimeOffset(expires).ToUnixTimeMilliseconds(),
            Id = userId,
            Role = role ?? string.Empty,
            Claims = claims ?? EmptyClaims
        };
    }
}