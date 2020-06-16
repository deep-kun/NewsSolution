#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY NewsSolution.csproj .
RUN dotnet restore NewsSolution.csproj
COPY . .
RUN dotnet publish -c Release -o out


FROM build AS publish
RUN dotnet publish "./NewsSolution.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewsSolution.dll"]