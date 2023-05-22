FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ApiSearch/ApiSearch.csproj", "ApiSearch/"]
COPY ["Common/Common.csproj", "Common/"]
RUN dotnet restore "ApiSearch/ApiSearch.csproj"
COPY . .
WORKDIR "/src/ApiSearch"
RUN dotnet build "ApiSearch.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiSearch.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiSearch.dll"]
