# build svelte app
FROM node AS node-build
WORKDIR /ChengetaWebApp

# Copy everything
COPY . ./
RUN npm i
RUN npm run build

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /ChengetaWebApp

# Copy everything
COPY . ./

# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /ChengetaWebApp
COPY --from=node-build /ChengetaWebApp/fullchain.pem .
COPY --from=node-build /ChengetaWebApp/privkey.pem .
COPY --from=build-env /ChengetaWebApp/out .
COPY --from=build-env /ChengetaWebApp/.env .
COPY --from=node-build /ChengetaWebApp/wwwroot ./wwwroot
EXPOSE 5000
ENTRYPOINT ["dotnet", "ChengetaWebApp.dll"]