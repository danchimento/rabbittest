FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app
EXPOSE 80

# copy csproj and restore as distinct layers
COPY *.sln .
COPY card/*.csproj ./card/
RUN dotnet restore

# copy everything else and build app
COPY card/. ./card/
WORKDIR /app/card
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/card/out ./
ENTRYPOINT ["dotnet", "card.dll"]
