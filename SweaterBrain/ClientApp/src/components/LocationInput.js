import React, {useState, useEffect} from "react";
import {
	Button,
	Form,
	FormGroup,
	Input,
	Label
} from "reactstrap";

const LocationInput = ({onChangeCity}) => {

	const [inputValue, setInputValue] = useState("");
	const resetLocation = () => {
		localStorage.setItem("inputValue", "");
		onChangeCity("");
		setInputValue("");
		window.location.reload(false);
	}

	const onChangeHandler = event => {
		var trimmedValue = event.target.value.trim();
		setInputValue(trimmedValue);
		localStorage.setItem("inputValue", trimmedValue);
		onChangeCity(trimmedValue);
	};

	useEffect(() => {
		setInputValue(localStorage.getItem("inputValue"));
		onChangeCity(localStorage.getItem("inputValue"));
	}, []);


	return (
		<Form inline>
				<FormGroup>
					<Input type="string"
						name="locationInfo"
						id="locationInfo"
						placeholder="Beverly Hills, Ca?"
						value={inputValue}
						onChange={onChangeHandler}
					/>
				</FormGroup>
			<Button onClick={resetLocation}>Reset</Button>
		</Form>
	);
}

export default LocationInput;
