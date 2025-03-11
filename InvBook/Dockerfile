# Base image for runtime (used when running the container)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Ensure the database directory exists and set correct permissions
RUN mkdir -p /app/data && chmod -R 777 /app/data

# Image used for building the service
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the project file from the correct directory
COPY ["InvBook.csproj", "./"]
RUN dotnet restore "InvBook.csproj"

# Copy everything else
COPY . .
WORKDIR "/src"
RUN dotnet build "InvBook.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the project
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "InvBook.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Force Development mode inside the container
ENV ASPNETCORE_ENVIRONMENT=Development

# Ensure the database directory exists in the final container
RUN mkdir -p /app/data && chmod -R 777 /app/data

# Ensure database migrations are applied before running the app
RUN dotnet ef database update || echo "Migration failed. Ensure the database is accessible."

# Run the application
ENTRYPOINT ["dotnet", "InvBook.dll"]
