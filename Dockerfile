# Use the official ASP.NET runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["AuthenticationProject.csproj", "./"]
RUN dotnet restore "AuthenticationProject.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "AuthenticationProject.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthenticationProject.csproj" -c Release -o /app/publish

# Copy the build files into the runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthenticationProject.dll"]
