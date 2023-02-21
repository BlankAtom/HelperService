FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ETWHost/ETWHost.csproj", "ETWHost/"]
RUN dotnet restore "ETWHost/ETWHost.csproj"
COPY . .
WORKDIR "/src/ETWHost"
RUN dotnet build "ETWHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ETWHost.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ETWHost.dll"]
