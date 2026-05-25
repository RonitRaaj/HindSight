# ==========================================
# 1. BUILD & COMPILE STAGE
# ==========================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy absolutely everything from your repository into the container workspace
COPY . ./

# 🚀 BULLETPROOF FIX: We bypass the .slnx solution file entirely! 
# We tell .NET to restore and publish by pointing directly to the WebAPI project file.
RUN dotnet restore src/Hindsight.WebAPI/Hindsight.WebAPI.csproj
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