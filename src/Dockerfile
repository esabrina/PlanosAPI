#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
COPY PlanosAPI/*.sln ./PlanosAPI/
COPY ["PlanosAPI/PlanosAPI.csproj", "PlanosAPI/"]
COPY PlanosAPI.Test/*.csproj ./PlanosAPI.Test/
RUN dotnet restore ./PlanosAPI/

# build
COPY . .
RUN dotnet build ./PlanosAPI/ -c Release -o /app/build

# run tests
FROM build AS test
WORKDIR /PlanosAPI.Test
RUN dotnet test --logger:trx

# publish 
FROM build AS publish
WORKDIR /PlanosAPI
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "PlanosAPI.dll"]

