# Используем официальные образы .NET 7.0 (или актуальную версию)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Сборка приложения с использованием SDK образа
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY WebApplication1.csproj ./WebApplication1/
RUN dotnet restore "WebApplication1/WebApplication1.csproj"
COPY . . 
WORKDIR "/src/WebApplication1"
RUN dotnet build "WebApplication1.csproj" -c Release -o /app/build

# Публикация приложения
FROM build AS publish
RUN dotnet publish "WebApplication1.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Финальный образ
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish . 
ENTRYPOINT ["dotnet", "WebApplication1.dll"]

FROM mcr.microsoft.com/dotnet/core/sdk:6.0
WORKDIR /app
COPY . .
RUN dotnet tool install --global dotnet-ef

