FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7050

ENV ASPNETCORE_URLS=http://+:7050

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["IoTDevices.csproj", "./"]
RUN dotnet restore "IoTDevices.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "IoTDevices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IoTDevices.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IoTDevices.dll"]
