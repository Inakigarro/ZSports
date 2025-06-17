import { Component, Input, input, output } from "@angular/core";
import { Button, defaultButton } from "../shared/button/button.models";
import { ButtonComponent } from "../button/button.component";
import { Action } from "@ngrx/store";

@Component({
	selector: "app-topbar",
	templateUrl: "./topbar.component.html",
	styleUrl: "./topbar.component.scss",
	standalone: true,
	imports: [ButtonComponent],
	providers: [],
})
export class TopbarComponent {
	// Default button configuration
	initButton = defaultButton;

	id = input.required<string>();
	title = input.required<string>();
	mainButtonClicked = output<Action>();
	secondaryButtonClicked = output<Action>();

	@Input() mainButton: Button = defaultButton;
	@Input() secondaryButtons: Button[] = [];
}
