#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TicketProcessor.Microservice/TicketProcessor.Microservice.csproj", "TicketProcessor.Microservice/"]
COPY ["Shared.Models/Shared.Models.csproj", "Shared.Models/"]
RUN dotnet restore "TicketProcessor.Microservice/TicketProcessor.Microservice.csproj"
COPY . .
WORKDIR "/src/TicketProcessor.Microservice"
RUN dotnet build "TicketProcessor.Microservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TicketProcessor.Microservice.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicketProcessor.Microservice.dll"]