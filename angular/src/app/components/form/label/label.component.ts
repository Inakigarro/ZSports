import { Component, input } from "@angular/core";

@Component({
	selector: "app-label",
	standalone: true,
	templateUrl: "./label.component.html",
	styleUrl: "./label.component.scss",
	imports: [],
})
export class LabelComponent {
	formControlId = input.required<string>();
	label = input.required<string>();
}
