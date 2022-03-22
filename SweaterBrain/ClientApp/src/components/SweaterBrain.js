import React, { Component } from 'react';
import {
    Card,
    CardBody,
    CardTitle,
    CardSubtitle,
} from 'reactstrap';

export class SweaterBrain extends Component {
    static displayName = SweaterBrain.name;

        constructor(props) {
            super(props);
            this.state = {
                temperature: "",
                weight: "",
                sweaterPath: "",
                loading: false
            };


        }

        componentDidMount() {
            this.populateWeatherData();
        }


    static renderForecastsTable(temperature, weight, sweaterPath) {

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
                            {weight} Weight due to a current temperature of {temperature}°
                        </CardSubtitle>
                        <img src={`${sweaterPath}`} width="20%" height="20%" alt="sweater" />
                    </CardBody>
                </Card>
            </div>
        );
    }

        render() {
            let contents = this.state.loading
              ? <p><em>Loading...</em></p>
                : SweaterBrain.renderForecastsTable(
                    this.state.temperature,
                    this.state.weight,
                    this.state.sweaterPath,
                );

        return (
          <div>
            <h1 id="tabelLabel" >Current Suggestion</h1>
                <p>Todays suggested sweater weight for Los Angeles California is...</p>
            {contents}
          </div>
        );
    }

    async populateWeatherData(importAll) {
        const response = await fetch('weatherforecast/suggester-data');
        const data = await response.json();

        this.setState({
            temperature: data.temp,
            weight: data.weight,
            sweaterPath: data.sweaterPath,
            loading: false
        });
    }
}
