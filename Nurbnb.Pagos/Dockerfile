FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app  
EXPOSE 32035  

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Nurbnb.Pagos.WebAPI/Nurbnb.Pagos.WebAPI.csproj", "/Nurbnb.Pagos.WebAPI/"]  
COPY . .
RUN dotnet build "Nurbnb.Pagos.WebAPI/Nurbnb.Pagos.WebAPI.csproj" -c Release -o /app  

FROM build AS publish
RUN dotnet publish "Nurbnb.Pagos.WebAPI/Nurbnb.Pagos.WebAPI.csproj" -c Release -o /app

#FROM mcr.microsoft.com/dotnet/aspnet:6.0
FROM base AS final
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Nurbnb.Pagos.WebAPI.dll"]