﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Users.Core.DTO;
using Confab.Modules.Users.Core.Entities;
using Confab.Modules.Users.Core.Events;
using Confab.Modules.Users.Core.Exceptions;
using Confab.Modules.Users.Core.Mappings;
using Confab.Modules.Users.Core.Repositories;
using Confab.Shared.Abstractions;
using Confab.Shared.Abstractions.Auth;
using Confab.Shared.Abstractions.Messaging;
using Microsoft.AspNetCore.Identity;

namespace Confab.Modules.Users.Core.Services;

internal class IdentityService : IIdentityService
{
    private readonly IAuthManager _authManager;
    private readonly IClock _clock;
    private readonly IMessageBroker _messageBroker;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IUserRepository _userRepository;

    public IdentityService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher,
        IAuthManager authManager, IClock clock, IMessageBroker messageBroker)
    {
        _authManager = authManager;
        _clock = clock;
        _messageBroker = messageBroker;
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
    }

    public async Task<AccountDto> GetAsync(Guid id)
    {
        var user = await _userRepository.GetAsync(id);
        return user?.AsDto();
    }

    public async Task<JsonWebToken> SignInAsync(SignInDto dto)
    {
        var user = await _userRepository.GetAsync(dto.Email.ToLowerInvariant());
        if (user == null)
            throw new InvalidCredentialException();

        var verificationResult = _passwordHasher.VerifyHashedPassword(user: default, user.Password, dto.Password);
        if (verificationResult == PasswordVerificationResult.Failed)
            throw new InvalidCredentialException();

        if (!user.IsActive)
            throw new UserNotActiveException(user.Id);

        var jwt = _authManager.CreateToken(userId: user.Id.ToString(), user.Role, claims: user.Claims);
        jwt.Email = user.Email;
        await _messageBroker.PublishAsync(new SignedIn(user.Id, user.Email));

        return jwt;
    }

    public async Task SignUpAsync(SignUpDto dto)
    {
        var email = dto.Email.ToLowerInvariant();
        var user = await _userRepository.GetAsync(email);
        if (user is not null)
            throw new EmailInUseException(email);

        dto.Id = Guid.NewGuid();
        var password = _passwordHasher.HashPassword(user: default, dto.Password);
        user = new()
        {
            Id = dto.Id,
            Email = email,
            Password = password,
            Role = dto.Role?.ToLowerInvariant() ?? "user",
            CreatedAt = _clock.CurrentDate(),
            IsActive = true,
            Claims = dto.Claims ?? new Dictionary<string, IEnumerable<string>>()
        };

        await _userRepository.AddAsync(user);
        await _messageBroker.PublishAsync(new SignedUp(user.Id, user.Email));
    }
}