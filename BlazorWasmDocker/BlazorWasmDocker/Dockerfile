FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build

EXPOSE 5190
ENV ASPNETCORE_URLS=http://+:5190

ARG configuration=Release

WORKDIR /src

COPY ["BlazorWasmDocker.csproj", "BlazorWasmDocker/"]
RUN dotnet restore "BlazorWasmDocker/BlazorWasmDocker.csproj"

WORKDIR "/src/BlazorWasmDocker"
COPY . .
RUN dotnet build "BlazorWasmDocker.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "BlazorWasmDocker.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf