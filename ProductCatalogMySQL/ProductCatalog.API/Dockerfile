FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5164

ENV ASPNETCORE_URLS=http://+:5164

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["ProductCatalog.API/ProductCatalog.API.csproj", "ProductCatalog.API/"]
RUN dotnet restore "ProductCatalog.API/ProductCatalog.API.csproj"
COPY . .
WORKDIR "/src/ProductCatalog.API"
RUN dotnet build "ProductCatalog.API.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "ProductCatalog.API.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductCatalog.API.dll"]
