# ==========================================
# 1. BUILD & COMPILE STAGE
# ==========================================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env
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
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build-env /app/out .

# 🚀 FIXED: Allow the app to bind natively to port 8080 (the .NET standard)
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Hindsight.WebAPI.dll"]