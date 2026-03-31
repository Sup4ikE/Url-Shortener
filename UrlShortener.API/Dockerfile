# 1. Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY *.sln .

COPY UrlShortener.API/*.csproj ./UrlShortener.API/
COPY UrlShortener.Core/*.csproj ./UrlShortener.Core/
COPY UrlShortener.Infrastructure/*.csproj ./UrlShortener.Infrastructure/
COPY UrlShortener.Tests/*.csproj ./UrlShortener.Tests/

RUN dotnet restore

COPY . .

WORKDIR /src/UrlShortener.API
RUN dotnet publish -c Release -o /app/publish

# 2. Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "UrlShortener.API.dll"]