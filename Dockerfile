FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["UmbracoHack25/UmbracoHack25.csproj", "UmbracoHack25/"]
RUN dotnet restore "UmbracoHack25/UmbracoHack25.csproj"
COPY . .
WORKDIR "/src/UmbracoHack25"
RUN dotnet build "UmbracoHack25.csproj" -c  $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "UmbracoHack25.csproj" -c  $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UmbracoHack25.dll"]
