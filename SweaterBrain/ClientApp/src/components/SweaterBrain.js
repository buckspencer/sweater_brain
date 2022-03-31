import React, { Component } from "react";
import {
	Card,
	CardBody,
	CardSubtitle,
	Form,
	FormGroup,
	Label,
	Input,
} from "reactstrap";
import LocationInput, { CityDropdown } from "./LocationInput";

export class SweaterBrain extends Component {
	static displayName = SweaterBrain.name;

	constructor(props) {
		super(props);
		this.onChangeCity = this.onChangeCity.bind(this);
		this.state = {
			locationArea: "",
			temperature: "",
			weight: "",
			sweaterPath: "",
			majorCityLocationDto: {},
			loading: false,
		};
	}

	componentDidMount() {
		var storedLocation =
			localStorage.getItem("inputValue") || "Beverly Hills, Ca";
		this.populateWeatherData(storedLocation);
	}

	onChangeCity(locationInfo) {
		this.populateWeatherData(locationInfo);
	}

	static renderForecastsTable(
		temperature,
		weight,
		sweaterPath,
		majorCityLocationDto
	) {
		return (
			<div>
				<Card className="col-12 text-center">
					<CardBody>
						<CardSubtitle className="mb-5" tag="h5">
							Were you looking for {majorCityLocationDto["cityName"]}?
						</CardSubtitle>

						<CardSubtitle className="mb-2 text-muted" tag="h6">
							We suggest a {weight} weight due to a current temperature of{" "}
							{temperature}°
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
				this.state.sweaterPath,
				this.state.majorCityLocationDto
			)
		);

		return (
			<div className="clearfix">
				<h1 id="tabelLabel" className="float-left pr-2 mb-2">
					Enter a location for a sweater suggestion:
				</h1>
				<div className="mt-2 float-right">
					<LocationInput onChangeCity={this.onChangeCity} />
				</div>
				{contents}
			</div>
		);
	}

	async populateWeatherData(locationInfo) {
		const response = await fetch(
			"weatherforecast/suggester-data?locationInfo=" + locationInfo
		);
		const data = await response.json();

		this.setState({
			temperature: data.temp,
			weight: data.weight,
			sweaterPath: data.sweaterPath,
			majorCityLocationDto: data.majorCityLocationDto,
			loading: false,
		});
	}
}
