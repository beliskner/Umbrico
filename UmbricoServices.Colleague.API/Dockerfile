# Use the SDK image to build and publish the website
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UmbricoServices.Colleague.API.csproj", "."]
RUN dotnet restore "UmbricoServices.Colleague.API.csproj"
COPY . .
RUN dotnet publish "UmbricoServices.Colleague.API.csproj" -c Release -o /app/publish

# Copy the published output to the final running image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final 
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "UmbricoServices.Colleague.API.dll"]