import { Component } from "@angular/core";
import { ButtonComponent } from "@app/components/button/button.component";
import { CardComponent } from "@app/components/card/card.component";
import { Button } from "@app/components/shared/button/button.models";

@Component({
	selector: "app-reservas-list",
	standalone: true,
	templateUrl: "./reservas-list.component.html",
	styleUrl: "./reservas-list.component.scss",
	imports: [ButtonComponent, CardComponent],
	providers: [],
})
export class ReservasListComponent {
	id: string = "reservas-list";
	title: string = "Reservas";

	nuevaReservaButton: Button = {
		id: "nueva-reserva-button",
		label: "Nueva Reserva",
		type: "primary",
		icon: "fa-solid fa-plus",
	};

	constructor() {}
}
