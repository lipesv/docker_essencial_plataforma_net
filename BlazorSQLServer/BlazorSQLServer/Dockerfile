FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5277

ENV ASPNETCORE_URLS=http://+:5277

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["BlazorSQLServer/BlazorSQLServer.csproj", "BlazorSQLServer/"]
RUN dotnet restore "BlazorSQLServer/BlazorSQLServer.csproj"
COPY . .
WORKDIR "/src/BlazorSQLServer"
RUN dotnet build "BlazorSQLServer.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "BlazorSQLServer.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorSQLServer.dll"]
