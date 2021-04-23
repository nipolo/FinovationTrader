#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["FinovationTraderService/API/FinovationTrader.API.csproj", "FinovationTraderService/API/"]
COPY ["FinovationTraderService/Dtos/FinovationTrader.Dtos.csproj", "FinovationTraderService/Dtos/"]
COPY ["FinovationTraderService/Application/FinovationTrader.Application.csproj", "FinovationTraderService/Application/"]
COPY ["FinovationTraderService/Data/FinovationTrader.Data.csproj", "FinovationTraderService/Data/"]
RUN dotnet restore "FinovationTraderService/API/FinovationTrader.API.csproj"
COPY . .
WORKDIR "/src/FinovationTraderService/API"
RUN dotnet build "FinovationTrader.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FinovationTrader.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinovationTrader.API.dll"]