#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Bootstrapper/Confab.Bootstrapper/Confab.Bootstrapper.csproj", "src/Bootstrapper/Confab.Bootstrapper/"]
COPY ["src/Modules/Tickets/Confab.Modules.Tickets.Api/Confab.Modules.Tickets.Api.csproj", "src/Modules/Tickets/Confab.Modules.Tickets.Api/"]
COPY ["src/Modules/Tickets/Confab.Modules.Tickets.Core/Confab.Modules.Tickets.Core.csproj", "src/Modules/Tickets/Confab.Modules.Tickets.Core/"]
COPY ["src/Shared/Confab.Shared.Infrastructure/Confab.Shared.Infrastructure.csproj", "src/Shared/Confab.Shared.Infrastructure/"]
COPY ["src/Shared/Confab.Shared.Abstractions/Confab.Shared.Abstractions.csproj", "src/Shared/Confab.Shared.Abstractions/"]
COPY ["src/Shared/Confab.Shared.Kernel/Confab.Shared.Kernel.csproj", "src/Shared/Confab.Shared.Kernel/"]
COPY ["src/Modules/Attendances/Confab.Modules.Attendances.Api/Confab.Modules.Attendances.Api.csproj", "src/Modules/Attendances/Confab.Modules.Attendances.Api/"]
COPY ["src/Modules/Attendances/Confab.Modules.Attendances.Infrastructure/Confab.Modules.Attendances.Infrastructure.csproj", "src/Modules/Attendances/Confab.Modules.Attendances.Infrastructure/"]
COPY ["src/Modules/Attendances/Confab.Modules.Attendances.Application/Confab.Modules.Attendances.Application.csproj", "src/Modules/Attendances/Confab.Modules.Attendances.Application/"]
COPY ["src/Modules/Attendances/Confab.Modules.Attendances.Domain/Confab.Modules.Attendances.Domain.csproj", "src/Modules/Attendances/Confab.Modules.Attendances.Domain/"]
COPY ["src/Modules/Speakers/Confab.Modules.Speakers.Api/Confab.Modules.Speakers.Api.csproj", "src/Modules/Speakers/Confab.Modules.Speakers.Api/"]
COPY ["src/Modules/Speakers/Confab.Modules.Speakers.Core/Confab.Modules.Speakers.Core.csproj", "src/Modules/Speakers/Confab.Modules.Speakers.Core/"]
COPY ["src/Modules/Agendas/Confab.Modules.Agendas.Api/Confab.Modules.Agendas.Api.csproj", "src/Modules/Agendas/Confab.Modules.Agendas.Api/"]
COPY ["src/Modules/Agendas/Confab.Modules.Agendas.Infrastructure/Confab.Modules.Agendas.Infrastructure.csproj", "src/Modules/Agendas/Confab.Modules.Agendas.Infrastructure/"]
COPY ["src/Modules/Agendas/Confab.Modules.Agendas.Application/Confab.Modules.Agendas.Application.csproj", "src/Modules/Agendas/Confab.Modules.Agendas.Application/"]
COPY ["src/Modules/Agendas/Confab.Modules.Agendas.Domain/Confab.Modules.Agendas.Domain.csproj", "src/Modules/Agendas/Confab.Modules.Agendas.Domain/"]
COPY ["src/Modules/Users/Confab.Modules.Users.Api/Confab.Modules.Users.Api.csproj", "src/Modules/Users/Confab.Modules.Users.Api/"]
COPY ["src/Modules/Users/Confab.Modules.Users.Core/Confab.Modules.Users.Core.csproj", "src/Modules/Users/Confab.Modules.Users.Core/"]
COPY ["src/Modules/Conferences/Confab.Modules.Conferences.Api/Confab.Modules.Conferences.Api.csproj", "src/Modules/Conferences/Confab.Modules.Conferences.Api/"]
COPY ["src/Modules/Conferences/Confab.Modules.Conferences.Core/Confab.Modules.Conferences.Core.csproj", "src/Modules/Conferences/Confab.Modules.Conferences.Core/"]
RUN dotnet restore "src/Bootstrapper/Confab.Bootstrapper/Confab.Bootstrapper.csproj"
COPY . .
WORKDIR "/src/src/Bootstrapper/Confab.Bootstrapper"
RUN dotnet build "Confab.Bootstrapper.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Confab.Bootstrapper.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Confab.Bootstrapper.dll"]