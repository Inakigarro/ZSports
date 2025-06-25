import { Component, input, OnDestroy, OnInit, output } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ButtonIconPosition, ButtonType } from "../shared/button/button.models";
import { buttonClicked } from "../shared/button/button.actions";
import { Action } from "@ngrx/store";
import { Subject } from "rxjs";

@Component({
	selector: "app-button",
	standalone: true,
	imports: [CommonModule],
	templateUrl: "./button.component.html",
	styleUrl: "./button.component.scss",
})
export class ButtonComponent implements OnInit, OnDestroy {
	id = input.required<string>();
	hasIcon = input<boolean>(false);
	label = input<string>();
	icon = input<string>();
	iconPosition = input<ButtonIconPosition>("left");
	type = input<ButtonType>("primary");
	disabled = input<boolean>(false);
	hideLabelOnMobile = input<boolean>(false);
	onClick = output<Action>();

	protected isMobile: boolean = false;
	private resizeListener = () => {
		this.isMobile = window.matchMedia("(max-width: 600px)").matches;
	};

	ngOnInit() {
		this.resizeListener();
		window.addEventListener("resize", this.resizeListener);
	}

	ngOnDestroy() {
		window.removeEventListener("resize", this.resizeListener);
	}

	protected onButtonClick(): void {
		this.onClick.emit(buttonClicked({ id: this.id() }));
	}
}
