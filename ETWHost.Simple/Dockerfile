FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ETWHost.Simple/ETWHost.Simple.csproj", "ETWHost.Simple/"]
RUN dotnet restore "ETWHost.Simple/ETWHost.Simple.csproj"
COPY . .
WORKDIR "/src/ETWHost.Simple"
RUN dotnet build "ETWHost.Simple.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ETWHost.Simple.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ETWHost.Simple.dll"]
