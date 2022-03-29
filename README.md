# Project name: Sweater Brain
## View deployed app: [SweaterBrain](https://sweater-brain.herokuapp.com/)
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

<img width="977" alt="SweaterBrain Home" src="https://user-images.githubusercontent.com/5303892/160012509-ea7f99f9-d783-467b-8171-cbe1e318f998.png">

## To run app locally:
- Remove the 'example' from secrets.json and fill in the required values.
- App runs normally through visual studio after build.

## To run in Docker:
#### From solution file:

- First build:
```docker-compose build```
- To start:
```docker-compose up```
- To stop:
```docker-compose down```

## Heroku Deploy:
#### [Heroku deploy documentation] (https://devcenter.heroku.com/articles/git)
- Connect repository to Heroku app, set config vars with values from secrets.json, then connect via Git deployments.

