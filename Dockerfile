#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview AS base => Fehlerquelle f�r:
# Zielprozess wurde beendet, ohne ein Ereignis zum Start von CoreCLR ausgel�st wurde.
# Stellen Sie sicher, dass der Zielprozess f�r die Verwendung von .NET Core konfiguriert ist.
# Dies wird m�glicherweise erwartet, wenn der Zielprozess nicht unter .NET-Code ausgef�hrt wurde.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# MCR = Microsoft Container Registry
# FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ChristCodingChallengeBackend/ChristCodingChallengeBackend.csproj", "ChristCodingChallengeBackend/"]
RUN dotnet restore "ChristCodingChallengeBackend/ChristCodingChallengeBackend.csproj"
COPY . .
WORKDIR "/src/ChristCodingChallengeBackend"
RUN dotnet build "ChristCodingChallengeBackend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChristCodingChallengeBackend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ChristCodingChallengeBackend.dll"]