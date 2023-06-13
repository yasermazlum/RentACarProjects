FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
#COPY ["WebAPI.csproj", "WebAPI/"]
#COPY ["Application.csproj", "Application/"]
#COPY ["Domain.csproj", "Domain/"]
#COPY ["Infrastructure.csproj", "Infrastructure/"]
#COPY ["Persistence.csproj", "Persistence/"]
COPY . .
RUN dotnet restore "src/rentACar/WebAPI/WebAPI.csproj"
WORKDIR "src/rentACar/WebAPI/"
COPY . .
RUN dotnet build "WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAPI.dll"]