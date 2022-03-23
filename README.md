# Project name: Sweater Brain
- A simple C#/React appplication that displays sweater suggestions based on selected city's temperature.

- Link to Trello board for Agile project management information:
[Agile Board](https://trello.com/b/s4tz37nX/sweater-brain)

## Techinical description of project:
This codebase reflects the use of calling an external API, doing calculations, and dynamically rendering various images and text on a JavaScript client.

## Technologies:

Languages:
- JS
- C#

Frameworks:
- React
- .Net

Tests:
- xUnit


### Screenshots:

<img width="977" alt="Screen Shot 2022-03-03 at 1 06 01 PM" src="https://user-images.githubusercontent.com/5303892/156646231-47e26ded-025e-4ced-93a8-d2adf69b23ed.png">


## Environment Variables:

- ```API_KEY={OPEN WEATHER API KEY}```

## To run app locally:
- App runs normally through visual studio, all that is needed is to set the environment variable for the Api.
## Heroku Deploy:
- [Heroku deploy documentation] (https://devcenter.heroku.com/articles/git)
- Connect repository to Heroku app, set Environment Variable for Api, then connect via Git deployments.
## To run in Docker:
- Add ```docker-compose.override.yml``` to solution directory.
```
version: '3.4'

services:
  sweaterbrain:
    environment:
      - ASPNETCORE_ENVIRONMENT=Developement
      - API_KEY={OPEN WEATHER API KEY}
    ports:
      - 80
    volumes:
      - ~/.aspnet/https:/root/.aspnet/https:ro
```


- From solution file:

- First build:
```docker-compose build```

- To start:
```docker-compose up```
- To stop:
```docker-compose down```
