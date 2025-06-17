import { Component } from "@angular/core";
import { RouterOutlet } from "@angular/router";
import { ButtonComponent } from "./components/button/button.component";
import { InputComponent } from "./components/form/input/input.component";
import { Action, Store } from "@ngrx/store";
import {
	FormControl,
	FormGroup,
	FormsModule,
	ReactiveFormsModule,
	UntypedFormGroup,
	Validators,
} from "@angular/forms";
import { Button } from "./components/shared/button/button.models";
import { buttonClicked } from "./components/shared/button/button.actions";
import { LabelComponent } from "./components/form/label/label.component";
import {
	SelectComponent,
	SelectOption,
} from "./components/form/select/select.component";
import { TopbarComponent } from "./components/topbar/topbar.component";
import { DatePickerComponent } from "./components/form/date-picker/date-picker.component";
import { NavbarComponent } from "./components/navbar/navbar.component";
import { Topbar } from "./components/shared/topbar/topbar.models";
import { NavItemComponent } from "./components/navbar/nav-item/nav-item.component";

@Component({
	selector: "app-root",
	imports: [
		RouterOutlet,
		TopbarComponent,
		NavbarComponent,
		NavItemComponent,
		FormsModule,
		ReactiveFormsModule,
	],
	templateUrl: "./app.component.html",
	styleUrl: "./app.component.scss",
})
export class AppComponent {
	shellTopbar: Topbar = {
		id: "shell-topbar",
		title: "ZSports - C del U",
		mainButton: {
			id: "boton-menu",
			label: "Menu",
			type: "primary",
			icon: "fa-solid fa-bars",
		},
		secondaryButtons: [
			{
				id: "boton-acerca-de",
				label: "Acerca de",
				type: "secondary",
				icon: "fa-solid fa-info-circle",
			},
			{
				id: "boton-contacto",
				label: "Contacto",
				type: "secondary",
				icon: "fa-solid fa-envelope",
			},
		],
	};
	title = "angular";
	form: UntypedFormGroup;
	canchasOptions: SelectOption[] = [
		{ label: "Seleccione una cancha", value: 0 },
		{ label: "Cancha 1", value: 1 },
		{ label: "Cancha 2", value: 2 },
		{ label: "Cancha 3", value: 3 },
	];

	protected cancelarButton: Button = {
		id: "boton-cancelar",
		label: "Cancelar",
		type: "danger",
		icon: "fa-solid fa-close",
		iconPosition: "left",
	};

	protected reservarButton: Button = {
		id: "boton-reservar",
		label: "Reservar",
		type: "primary",
		icon: "fa-solid fa-calendar-check",
		iconPosition: "left",
	};

	constructor(private store: Store) {
		this.form = new FormGroup({
			nombre: new FormControl<string>("", [
				Validators.required,
				Validators.minLength(3),
			]),
			apellido: new FormControl<string>("", [
				Validators.required,
				Validators.minLength(3),
			]),
			nroCancha: new FormControl<number>(0, [
				Validators.required,
				Validators.min(1),
			]),
			fecha: new FormControl<Date | null>(null, [Validators.required]),
		});
	}

	onButtonClick(action: Action): void {
		const actionData = action as ReturnType<typeof buttonClicked>;

		if (actionData.id === this.cancelarButton.id) {
			this.form.get("nombre")?.setValue("");
			this.form.get("apellido")?.setValue("");
			this.form.get("nroCancha")?.setValue(this.canchasOptions[0].value);
			this.form.get("fecha")?.setValue(null);
		}

		if (actionData.id === this.reservarButton.id) {
			const data = this.form.value;
			console.log("Datos del formulario:", data);
		}
	}
}
