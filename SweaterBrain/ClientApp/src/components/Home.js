import React, { Component } from "react";

export class Home extends Component {
	static displayName = Home.name;

	render() {
		return (
			<div>
				<h1>Future Features:</h1>
				<p>
					As we inevitably progress on this piece of modern ingenuity we hope to
					see such functional augmentations as:
				</p>
				Advanced Sweater search with:
				<ul>
					<li>
						Search by location, includes calculations incorporating cultural and
						temperature based sensitivities
					</li>
					<li>Search by time of day</li>
					<li>Search by event type</li>
					<li>
						Randomizer incorporates safety guardrails so that you are not
						completely left in the wrong sweater. ie, a heavy sweater on a hot
						day.
					</li>
				</ul>
			</div>
		);
	}
}
