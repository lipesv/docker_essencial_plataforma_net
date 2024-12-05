# Use a imagem base do .NET 8 Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Configure o diretório de trabalho no container
WORKDIR /app

# Copie os arquivos publicados para o diretório de trabalho
COPY ./publish/ .

# Comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "mvc1.dll"]