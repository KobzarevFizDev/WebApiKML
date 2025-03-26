FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source
COPY WebApiKML.sln .
COPY WebApiKML/WebApiKML.csproj WebApiKML/
RUN dotnet restore

COPY WebApiKML/. WebApiKML/
WORKDIR /source/WebApiKML
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./
COPY --from=build /source/WebApiKML/WebApiKML.http ./
COPY --from=build /source/WebApiKML/appsettings.json ./
COPY --from=build /source/WebApiKML/Resources ./Resources
ENTRYPOINT ["dotnet", "WebApiKML.dll"]