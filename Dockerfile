# ==========================================
# 1. BUILD & COMPILE STAGE
# ==========================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# 🚀 FIX: Copy everything directly to completely avoid folder casing mismatch errors
COPY . ./
RUN dotnet restore

# Publish the final production release binaries
RUN dotnet publish src/Hindsight.WebAPI/Hindsight.WebAPI.csproj -c Release -o out

# ==========================================
# 2. RUNTIME STAGE
# ==========================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Ensure SQLite has read/write filesystem access within Render's app sandbox root
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "Hindsight.WebAPI.dll"]