﻿FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["ConsimpleWeb/ConsimpleWeb.csproj", "ConsimpleWeb/"]
RUN dotnet restore "ConsimpleWeb/ConsimpleWeb.csproj"
COPY . .
WORKDIR "/src/ConsimpleWeb"
RUN dotnet build "ConsimpleWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConsimpleWeb.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConsimpleWeb.dll"]