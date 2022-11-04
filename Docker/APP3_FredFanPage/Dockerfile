#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["APP3_FredFanPage/APP3_FredFanPage.csproj", "APP3_FredFanPage/"]
RUN dotnet restore "APP3_FredFanPage/APP3_FredFanPage.csproj"
COPY "APP3_FredFanPage/" "APP3_FredFanPage/"
WORKDIR "/src/APP3_FredFanPage"
RUN dotnet build "APP3_FredFanPage.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "APP3_FredFanPage.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["APP3_FredFanPage/FredFanPage.db", "."]
RUN apt-get -y update
RUN apt-get install -y sqlite3 libsqlite3-dev
RUN /usr/bin/sqlite3 ./FredFanPage.db
ENTRYPOINT ["dotnet", "APP3_FredFanPage.dll"]