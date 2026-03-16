## See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
## This stage is used when running from VS in fast mode (Default for Debug configuration)
#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#USER $APP_UID
#WORKDIR /app
#EXPOSE 8080
#
#
## This stage is used to build the service project
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["Ecommerce.csproj", "."]
#RUN dotnet restore "./Ecommerce.csproj"
#COPY . .
#WORKDIR "/src/."
#RUN dotnet build "./Ecommerce.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
## This stage is used to publish the service project to be copied to the final stage
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./Ecommerce.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
## This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Ecommerce.dll"]


# Stage 1: Build

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Ecommerce.csproj", "."]
RUN dotnet restore "Ecommerce.csproj"
COPY . .
RUN dotnet build "Ecommerce.csproj" -c Release -o /app/build


# Stage 2: Publish

FROM build AS publish
RUN dotnet publish "Ecommerce.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 8080
ENTRYPOINT ["dotnet", "Ecommerce.dll"]