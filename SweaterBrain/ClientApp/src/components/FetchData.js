import React, { Component } from 'react';
import { Dropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';

export class FetchData extends Component {
  static displayName = FetchData.name;

    constructor(props) {
        super(props);
        this.toggle = this.toggle.bind(this);
        this.state = {
            dropdownOpen: false,
            forecasts: [],
            loading: true,
            locationCords: []
        };
    }

  componentDidMount() {
    this.populateWeatherData();
    this.populateLocationData();
  }

 
    static renderForecastsTable(dropdown, forecasts, locationCords, toggle) {
        toggle() {
            this.setState(prevState => ({
                dropdownOpen: !prevState.dropdownOpen
            }));
        }

      return (
          <div>
              <Dropdown isOpen={dropdown} toggle={toggle}>
                  <DropdownToggle caret>
                      Dropdown
                  </DropdownToggle>
                  <DropdownMenu>
                      <DropdownItem header>Header</DropdownItem>
                      <DropdownItem disabled>Action</DropdownItem>
                      <DropdownItem>Another Action</DropdownItem>
                      <DropdownItem divider />
                      <DropdownItem>Another Action</DropdownItem>
                  </DropdownMenu>
              </Dropdown>
          <table className='table table-striped' aria-labelledby="tabelLabel">
            <thead>
              <tr>
                <th>Date</th>
                <th>Temp. (C)</th>
                <th>Temp. (F)</th>
                <th>Summary</th>
              </tr>
            </thead>
            <tbody>
              {forecasts.map(forecast =>
                <tr key={forecast.date}>
                  <td>{forecast.date}</td>
                  <td>{forecast.temperatureC}</td>
                  <td>{forecast.temperatureF}</td>
                  <td>{forecast.summary}</td>
                </tr>
              )}
            </tbody>
          </table>
        </div>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
        : FetchData.renderForecastsTable(this.state.dropdownOpen, this.state.forecasts, this.state.locationCords, this.state.toggle);

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

  async populateLocationData() {
    const response = await fetch('weatherforecast/locationcords');
    const data = await response.json();
    this.setState({ locationCords: data, loading: false });
  }
}
