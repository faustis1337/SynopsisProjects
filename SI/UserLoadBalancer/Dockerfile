FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["UserLoadBalancer/UserLoadBalancer.csproj", "UserLoadBalancer/"]
RUN dotnet restore "UserLoadBalancer/UserLoadBalancer.csproj"
COPY . .
WORKDIR "/src/UserLoadBalancer"
RUN dotnet build "UserLoadBalancer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserLoadBalancer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserLoadBalancer.dll"]
