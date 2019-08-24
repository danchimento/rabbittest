FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app
EXPOSE 80
EXPOSE 443

WORKDIR /src
COPY . .
RUN dotnet restore "Orchestrator.web/Orchestrator.web.csproj"
COPY . .
WORKDIR "/src/Orchestrator.web"
RUN dotnet build "Orchestrator.web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Orchestrator.web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Orchestrator.web.dll"]