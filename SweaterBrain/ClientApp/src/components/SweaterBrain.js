import React, { Component } from 'react';
import {
    Button,
    Card,
    CardBody,
    CardTitle,
    CardSubtitle,
    CardText
} from 'reactstrap';



export class SweaterBrain extends Component {
    static displayName = SweaterBrain.name;

  constructor(props) {
    super(props);
    this.state = { forecasts: [], sweaterUrl: "", loading: true };
  }

  componentDidMount() {
      this.populateWeatherData();
      this.getTodaysSweater();
  }

    static renderForecastsTable(forecasts) {

        return (
            <div>
                <Card className="col-12 text-center">
                    <CardBody>
                        <CardTitle tag="h5">
                            
                        </CardTitle>
                        <CardSubtitle
                            className="mb-2 text-muted"
                            tag="h6"
                        >
                            Medium Weight do to a current temperature of  [TEMPERATURE HERE]
                        </CardSubtitle>
                        <img src={require('../images/medium_sweater.png').default} width="20%" height="20%" />
                    </CardBody>
                </Card>
            </div>
        );
    }

  render() {
    let contents = SweaterBrain.renderForecastsTable(this.state.forecasts);
    //let contents = this.state.loading
    //  ? <p><em>Loading...</em></p>
    //  : SweaterBrain.renderForecastsTable(this.state.forecasts);

    return (
      <div>
        <h1 id="tabelLabel" >Current Suggestion</h1>
            <p>Todays suggested sweater weight for Los Angeles California is...</p>
        {contents}
      </div>
    );
  }

  async populateWeatherData() {
    const response = await fetch('weatherforecast/list');
    const data = await response.json();
    this.setState({ forecasts: data, loading: false });
    }


  async getTodaysSweater() {
    const response = await fetch('weatherforecast/sweaterresult');
    const data = await response.json();
    this.setState({ sweaterUrl: data, loading: false });
    console.log(data);
  }
}
