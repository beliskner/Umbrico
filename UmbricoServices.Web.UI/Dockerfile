# Use the SDK image to build and publish the website
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UmbricoServices.Web.UI.csproj", "."]
RUN dotnet restore "UmbricoServices.Web.UI.csproj"
COPY . .
RUN dotnet publish "UmbricoServices.Web.UI.csproj" -c Release -o /app/publish

# Copy the published output to the final running image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final 
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "UmbricoServices.Web.UI.dll"]