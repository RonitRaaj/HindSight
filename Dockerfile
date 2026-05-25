# ==========================================
# 1. BUILD & COMPILE STAGE
# ==========================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

// Copy the solution and project files first to leverage Docker layer caching
COPY *.sln ./
COPY src/Hindsight.Core/*.csproj ./src/Hindsight.Core/
COPY src/Hindsight.Application/*.csproj ./src/Hindsight.Application/
COPY src/Hindsight.Infrastructure/*.csproj ./src/Hindsight.Infrastructure/
COPY src/Hindsight.WebAPI/*.csproj ./src/Hindsight.WebAPI/
RUN dotnet restore

// Copy the remaining source directories and publish the final binaries
COPY src/ ./src/
RUN dotnet publish src/Hindsight.WebAPI/Hindsight.WebAPI.csproj -c Release -o out

# ==========================================
# 2. RUNTIME STAGE
# ==========================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

// Ensure SQLite has read/write filesystem access within Render's app sandbox root
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "Hindsight.WebAPI.dll"]