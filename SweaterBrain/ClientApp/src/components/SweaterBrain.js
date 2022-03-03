import React, { Component } from 'react';
import ReactHtmlParser from 'react-html-parser'

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
            this.state = {
                temperature: "",
                feelsLike: "",
                weight: "",
                sweaterPath: "",
                images: [],
                loading: false
            };

            
        }

        componentDidMount() {
            var importAll = function importAll(r) {
                let images = [];
                r.keys().map(item => { images[item.replace('./', '')] = r(item); });
                return images;
            }
            this.populateWeatherData(importAll)
        }


    static renderForecastsTable(temperature, feelsLike, weight, sweaterPath, images) {

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
                    this.state.feelsLike,
                    this.state.weight,
                    this.state.sweaterPath,
                    this.state.images,
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
        const images = importAll(require.context('../images', false, /\.jpeg/));

        this.setState({
            images: images,
            temperature: data.temp,
            feelsLike: data.feelsLike,
            weight: data.weight,
            sweaterPath: images[`${data.sweaterPath}`].default,
            loading: false
        });
    }
}
