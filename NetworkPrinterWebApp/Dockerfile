FROM mcr.microsoft.com/dotnet/sdk:latest AS build-env

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY . ./

#clean all existing binaries
RUN find -type d -name bin -prune -exec rm -rf {} \; && find -type d -name obj -prune -exec rm -rf {} \;

#WORKDIR /app/NetworkPrinterWebApp
#RUN dotnet clean; dotnet restore --configfile ../NuGet.config

# Copy everything else and build
RUN dotnet publish -c Release -o out NetworkPrinterWebApp.csproj

EXPOSE 81

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:latest
WORKDIR /app
COPY --from=build-env /app/out/ .
ENTRYPOINT ["dotnet", "NetworkPrinterWebApp.dll"]