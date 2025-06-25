import { Component, input } from "@angular/core";
import { TopbarComponent } from "../topbar/topbar.component";

@Component({
	selector: "app-card",
	standalone: true,
	templateUrl: "./card.component.html",
	styleUrl: "./card.component.scss",
})
export class CardComponent {
	id = input.required<string>();
}
