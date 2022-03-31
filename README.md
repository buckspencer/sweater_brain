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

<img width="1136" alt="Screen Shot 2022-03-31 at 4 24 43 PM" src="https://user-images.githubusercontent.com/5303892/161165452-b48cd40a-98c8-4800-88b0-a71bd38828bb.png">

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

