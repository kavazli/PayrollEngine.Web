# Runtime stage - Use pre-built files from host
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080

# Copy published output from host machine
COPY ["PayrollEngine.Web.API/bin/Release/net9.0/publish/", "."]

ENTRYPOINT ["dotnet", "PayrollEngine.Web.API.dll"]

