FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore "EZPos.Web.Api/EZPos.Web.Api.csproj"
RUN dotnet publish "EZPos.Web.Api/EZPos.Web.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Render provides PORT at runtime; default to 10000 for local container runs.
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT:-10000}
EXPOSE 10000

ENTRYPOINT ["dotnet", "EZPos.Web.Api.dll"]
