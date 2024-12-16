# Use a base image with the .NET 8.0 runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the .NET 8.0 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["E2EDemoUserRegistration/E2EDemoUserRegistration.csproj", "E2EDemoUserRegistration/"]
RUN dotnet restore "E2EDemoUserRegistration/E2EDemoUserRegistration.csproj"

# Copy the rest of the application code
COPY E2EDemoUserRegistration/ E2EDemoUserRegistration/
WORKDIR "/src/E2EDemoUserRegistration"
RUN dotnet build "E2EDemoUserRegistration.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "E2EDemoUserRegistration.csproj" -c Release -o /app/publish

# Use the runtime image to run the app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "E2EDemoUserRegistration.dll"]