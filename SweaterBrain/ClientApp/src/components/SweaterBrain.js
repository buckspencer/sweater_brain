import React, { Component } from "react";
import { Card, CardBody, CardSubtitle } from "reactstrap";
import { CityDropdown } from "./CityDropdown";

export class SweaterBrain extends Component {
	static displayName = SweaterBrain.name;

	constructor(props) {
		super(props);
		this.onChangeCity = this.onChangeCity.bind(this);
		this.state = {
			temperature: "",
			weight: "",
			sweaterPath: "",
			loading: false,
		};
	}

	componentDidMount() {
		this.populateWeatherData();
	}

	onChangeCity(cityGeo) {
		this.populateWeatherData(cityGeo);
	}

	static renderForecastsTable(temperature, weight, sweaterPath) {
		return (
			<div>
				<Card className="col-12 text-center">
					<CardBody>
						<CardSubtitle className="mb-2 text-muted" tag="h6">
							{weight} Weight due to a current temperature of {temperature}°
						</CardSubtitle>
						<img
							src={`${sweaterPath}`}
							width="20%"
							height="20%"
							alt="sweater"
						/>
					</CardBody>
				</Card>
			</div>
		);
	}

	render() {
		let contents = this.state.loading ? (
			<p>
				<em>Loading...</em>
			</p>
		) : (
			SweaterBrain.renderForecastsTable(
				this.state.temperature,
				this.state.weight,
				this.state.sweaterPath
			)
		);

		return (
			<div className="clearfix">
				<h1 id="tabelLabel" className="float-left pr-2 mb-2">
					Today's current sweater suggestion is:
				</h1>
				<div className="mt-2 float-right">
					<CityDropdown onChangeCity={this.onChangeCity} />
				</div>
				{contents}
			</div>
		);
	}

	async populateWeatherData(cityGeo = ["34", "-118"]) {
		const response = await fetch(
			"weatherforecast/suggester-data?cityGeo=" + cityGeo
		);
		const data = await response.json();

		this.setState({
			temperature: data.temp,
			weight: data.weight,
			sweaterPath: data.sweaterPath,
			loading: false,
		});
	}
}
