FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

COPY *.csproj .

RUN dotnet restore

COPY . ./

RUN dotnet publish -c Debug -o out

FROM mcr.microsoft.com/dotnet/runtime:7.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "WebAPI.dll"]