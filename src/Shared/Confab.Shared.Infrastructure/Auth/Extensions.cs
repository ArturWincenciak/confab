using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Confab.Shared.Abstractions.Auth;
using Confab.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Confab.Shared.Infrastructure.Auth;

public static class Extensions
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IList<IModule> modules = null,
        Action<JwtBearerOptions> optionsFactory = null)
    {
        var options = services.GetOptions<AuthOptions>("auth");
        if (options.AuthenticationDisabled)
            services.AddSingleton<IPolicyEvaluator, DisabledAuthenticationPolicyEvaluator>();

        var tokenValidationParameters = new TokenValidationParameters
        {
            ClockSkew = TimeSpan.Zero,
            RequireAudience = options.RequireAudience,
            RequireExpirationTime = options.RequireExpirationTime,
            RequireSignedTokens = options.RequireSignedTokens,
            SaveSigninToken = options.SaveSigninToken,
            ValidateActor = options.ValidateActor,
            ValidateAudience = options.ValidateAudience,
            ValidateIssuer = options.ValidateIssuer,
            ValidateIssuerSigningKey = options.ValidateIssuerSigningKey,
            ValidateLifetime = options.ValidateLifetime,
            ValidateTokenReplay = options.ValidateTokenReplay,
            ValidAudience = options.ValidAudience,
            ValidAudiences = options.ValidAudiences,
            ValidIssuer = options.ValidIssuer,
            ValidIssuers = options.ValidIssuers
        };

        if (string.IsNullOrWhiteSpace(options.IssuerSigningKey))
            throw new ArgumentException(message: "Missing issuer signing key.",
                paramName: nameof(options.IssuerSigningKey));

        var rawKey = Encoding.UTF8.GetBytes(options.IssuerSigningKey);
        tokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(rawKey);

        if (!string.IsNullOrWhiteSpace(options.AuthenticationType))
            tokenValidationParameters.AuthenticationType = options.AuthenticationType;

        if (!string.IsNullOrWhiteSpace(options.NameClaimType))
            tokenValidationParameters.NameClaimType = options.NameClaimType;

        if (!string.IsNullOrWhiteSpace(options.RoleClaimType))
            tokenValidationParameters.RoleClaimType = options.RoleClaimType;

        services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.Authority = options.Authority;
            o.Audience = options.Audience;
            o.MetadataAddress = options.MetadataAddress;
            o.SaveToken = options.SaveToken;
            o.RefreshOnIssuerKeyNotFound = options.RefreshOnIssuerKeyNotFound;
            o.RequireHttpsMetadata = options.RequireHttpsMetadata;
            o.IncludeErrorDetails = options.IncludeErrorDetails;
            o.TokenValidationParameters = tokenValidationParameters;
            if (!string.IsNullOrWhiteSpace(options.Challenge))
                o.Challenge = options.Challenge;

            optionsFactory?.Invoke(o);
        });

        services.AddSingleton<IAuthManager, AuthManager>();
        services.AddSingleton(options);
        services.AddSingleton(tokenValidationParameters);

        var policies = modules?.SelectMany(x => x.Policies ?? Enumerable.Empty<string>()) ??
                       Enumerable.Empty<string>();
        services.AddAuthorization(authrization =>
        {
            foreach (var policy in policies)
                authrization.AddPolicy(policy,
                    configurePolicy: policyBuilder => policyBuilder.RequireClaim(claimType: "permissions", policy));
        });

        return services;
    }
}