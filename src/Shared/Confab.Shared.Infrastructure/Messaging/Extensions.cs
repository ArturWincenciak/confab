﻿using Confab.Shared.Abstractions.Messaging;
using Confab.Shared.Infrastructure.Messaging.Brokers;
using Confab.Shared.Infrastructure.Messaging.Dispatchers;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Messaging;

internal static class Extensions
{
    private const string Messaging = "messaging";

    internal static IServiceCollection AddMessaging(this IServiceCollection services)
    {
        services.AddSingleton<IMessageBroker, InMemoryMessageBroker>();
        services.AddSingleton<IMessageChannel, MessageChannel>();
        services.AddSingleton<IAsyncMessageDispatcher, AsyncMessageDispatcher>();

        var messagingOptions = services.GetOptions<MessagingOptions>(Messaging);
        services.AddSingleton(messagingOptions);

        if (messagingOptions.UseBackgroundDispatcher)
            services.AddHostedService<BackgroundDispatcher>();

        return services;
    }
}