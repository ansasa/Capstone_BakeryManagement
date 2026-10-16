
# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy source files, excluding bin/ and obj/ via .dockerignore
COPY . .

# Restore packages inside the Linux container
RUN dotnet restore "BakeryManagement.csproj"

# Publish the application
RUN dotnet publish "BakeryManagement.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore

# Stage 2: Run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_HTTP_PORTS=8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "BakeryManagement.dll"]












## Old code below

## Stage 1: Build the application
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#WORKDIR /src
#
## Copy project file and restore dependencies
#COPY ["BakeryManagement.csproj", "./"]
#RUN dotnet restore "BakeryManagement.csproj"
#
## Copy the remaining application files
#COPY . .
#RUN dotnet publish "BakeryManagement.csproj" \
    #-c Release \
    #-o /app/publish \
    #--no-restore
#
## Stage 2: Run the application
#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
#WORKDIR /app
#
#COPY --from=build /app/publish .
#
#ENV ASPNETCORE_HTTP_PORTS=8080
#
#EXPOSE 8080
#
#ENTRYPOINT ["dotnet", "BakeryManagement.dll"]
#