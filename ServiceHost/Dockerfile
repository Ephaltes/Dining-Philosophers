FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ServiceHost/ServiceHost.csproj", "ServiceHost/"]
RUN dotnet restore "ServiceHost/ServiceHost.csproj"
COPY . .
WORKDIR "/src/ServiceHost"
RUN dotnet build "ServiceHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ServiceHost.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServiceHost.dll"]
