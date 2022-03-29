import React from "react";
import {
	Dropdown,
	DropdownToggle,
	DropdownMenu,
	DropdownItem,
} from "reactstrap";

export class CityDropdown extends React.Component {
	constructor(props) {
		super(props);

		this.toggle = this.toggle.bind(this);
		this.select = this.select.bind(this);
		this.state = {
			dropdownOpen: false,
			locations: [],
			value: "Los Angeles, USA",
		};
	}
	componentDidMount() {
		this.populateCitySelector();
	}

	toggle() {
		this.setState((prevState) => ({
			dropdownOpen: !prevState.dropdownOpen,
		}));
	}

	select(event) {
		this.setState({
			dropdownOpen: !this.state.dropdownOpen,
			value: event.target.innerText,
		});
		this.props.onChangeCity(event.target.getAttribute("data-geo"));
	}

	async populateCitySelector() {
		const response = await fetch("weatherforecast/city-selection");
		const data = await response.json();
		this.setState({ locations: data });
	}

	render() {
		return (
			<Dropdown
				className="mt-3"
				isOpen={this.state.dropdownOpen}
				toggle={this.toggle}
			>
				<DropdownToggle caret>{this.state.value}</DropdownToggle>
				<DropdownMenu>
					{this.state.locations.map((location) => (
						<DropdownItem data-geo={location.latLon} onClick={this.select}>
							{location.cityName}
						</DropdownItem>
					))}
				</DropdownMenu>
			</Dropdown>
		);
	}
}
