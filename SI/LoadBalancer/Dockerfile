FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["LoadBalancer/LoadBalancer.csproj", "LoadBalancer/"]
COPY ["Common/Common.csproj", "Common/"]
RUN dotnet restore "LoadBalancer/LoadBalancer.csproj"
COPY . .
WORKDIR "/src/LoadBalancer"
RUN dotnet build "LoadBalancer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LoadBalancer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LoadBalancer.dll"]
