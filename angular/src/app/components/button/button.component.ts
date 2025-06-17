import { Component, input, output } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ButtonIconPosition, ButtonType } from "../shared/button/button.models";
import { buttonClicked } from "../shared/button/button.actions";
import { Action } from "@ngrx/store";

@Component({
	selector: "app-button",
	standalone: true,
	imports: [CommonModule],
	templateUrl: "./button.component.html",
	styleUrl: "./button.component.scss",
})
export class ButtonComponent {
	id = input.required<string>();
	hasIcon = input<boolean>(false);
	label = input<string>();
	icon = input<string>();
	iconPosition = input<ButtonIconPosition>("left");
	type = input<ButtonType>("primary");
	disabled = input<boolean>(false);
	onClick = output<Action>();

	protected onButtonClick(): void {
		this.onClick.emit(buttonClicked({ id: this.id() }));
	}
}
