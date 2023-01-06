# Project C group 1
[![forthebadge](https://forthebadge.com/images/badges/works-on-my-machine.svg)](https://forthebadge.com)

[![forthebadge](https://forthebadge.com/images/badges/built-with-love.svg)](https://forthebadge.com)

[![forthebadge](https://forthebadge.com/images/badges/fuck-it-ship-it.svg)](https://forthebadge.com)
## how to setup for development:
```
dotnet restore
npm install
npm run dev
```

## important files/folders:
- `wwwroot` contains all the static files
- `Api` contains all the api controllers, models and services
- `svelte-app` contains all the svelte code
- `Program.cs` contains the main method
- `Dockerfile` contains the dockerfile

## packages used:
- [Svelte](https://svelte.dev/)
- [Sqlite](https://www.sqlite.org/index.html)
- [Entity Framework](https://docs.microsoft.com/en-us/ef/core/)
- [Docker](https://www.docker.com/)
- [jwt_decoder](https://www.npmjs.com/package/jwt-decode)
- [Bcrypt](https://www.npmjs.com/package/bcrypt)
- [Babel](https://babeljs.io/)
- [Tailwind](https://tailwindcss.com/)
- [Leaflet](https://leafletjs.com/)
- [Jest](https://jestjs.io/)
- [Xunit](https://xunit.net/)

## website:
[https://www.thechengeta.com/](https://www.thechengeta.com/)

## how to deploy:
```
Generate ssl certificate using certbot

Run the following commands:
docker build -t chengeta .
docker run -d -p 80:80 -p 443:443 chengeta

```
 -----
## Group members:
- Gerrit van Aalst
- Mustafa Altun
- Esmee Bastiaanse
- Vincent de Gans