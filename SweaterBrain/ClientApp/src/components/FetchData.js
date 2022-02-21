import React, { Component } from 'react';
import {
    Button,
    Card,
    CardBody,
    CardTitle,
    CardSubtitle,
    CardText
} from 'reactstrap';



export class FetchData extends Component {
    static displayName = FetchData.name;

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
                <Card
                >
                    <CardBody>
                        <CardTitle tag="h5">
                            Today's sweater recommendation: 
                        </CardTitle>
                        <CardSubtitle
                            className="mb-2 text-muted"
                            tag="h6"
                        >
                            Card subtitle
                        </CardSubtitle>
                        <img src={require('../images/medium_sweater.png').default} width="20%" height="20%" />
                        <CardText>
                            Some quick example text to build on the card title and make up the bulk of the card's content.
                        </CardText>
                        <Button>
                            Button
                        </Button>
                    </CardBody>
                </Card>
            </div>
        );
    }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderForecastsTable(this.state.forecasts);

    return (
      <div>
        <h1 id="tabelLabel" >Weather forecast</h1>
        <p>This component demonstrates fetching data from the server.</p>
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
