# ==========================================
# 1. BUILD & COMPILE STAGE
# ==========================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy all repository source files into the container workspace
COPY . ./

# 🚀 FIXED: Point directly to your modern .slnx file for the restore process
RUN dotnet restore Hindsight.slnx

# Publish the final release binaries using the explicit WebAPI project path
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