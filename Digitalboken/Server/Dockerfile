FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Server/Digitalboken.Server.csproj", "Server/"]
RUN dotnet restore "Server/Digitalboken.Server.csproj"
COPY . .
WORKDIR "/src/Server"
RUN dotnet build "Digitalboken.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Digitalboken.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Digitalboken.Server.dll"]