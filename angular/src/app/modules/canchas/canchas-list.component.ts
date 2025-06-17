import { Component, OnDestroy, OnInit } from "@angular/core";
import { ButtonComponent } from "@app/components/button/button.component";
import { CardComponent } from "@app/components/card/card.component";
import { ListColumn, ListComponent } from "@app/components/list/list.component";
import { Button } from "@app/components/shared/button/button.models";
import { Cancha } from "./canchas.models";
import { CanchasService } from "./canchas.service";
import { CanchasActions } from "./state/canchas.actions";
import { Subject, takeUntil } from "rxjs";
import { CommonModule } from "@angular/common";
import { SideComponent } from "@app/components/side/side.component";
import { NuevaCanchaComponent } from "./components/nueva-cancha/nueva-cancha.component";

@Component({
	selector: "app-canchas-list",
	standalone: true,
	templateUrl: "./canchas-list.component.html",
	styleUrl: "./canchas-list.component.scss",
	imports: [
		ButtonComponent,
		CardComponent,
		CommonModule,
		ListComponent,
		SideComponent,
		NuevaCanchaComponent,
	],
	providers: [],
})
export class CanchasListComponent implements OnInit, OnDestroy {
	private destroy$ = new Subject<void>();
	id: string = "canchas";
	title: string = "Canchas";

	nuevaCanchaButton: Button = {
		id: "nueva-cancha-button",
		label: "Nueva Cancha",
		type: "primary",
		icon: "fa-solid fa-plus",
	};

	columns: ListColumn<Cancha>[] = [
		{
			key: "numero",
			label: "N°",
			width: "1rem",
			align: "center",
		},
		{
			key: "tipoSueloParseado",
			label: "Tipo de Suelo",
			align: "left",
		},
	];

	data: Cancha[] = [];

	protected canchasLoading$ = this.service.canchasLoading$;
	protected currentCancha$ = this.service.canchaActual$;
	protected nuevaCanchaOpened: boolean = false;

	constructor(private readonly service: CanchasService) {}

	ngOnInit(): void {
		this.service.dispatch(CanchasActions.iniciarCargaDeCanchas());
		this.service.canchas$
			.pipe(takeUntil(this.destroy$))
			.subscribe((canchas) => (this.data = canchas));
	}

	ngOnDestroy(): void {
		this.destroy$.next();
		this.destroy$.complete();
	}

	protected onBackdropClicked() {
		this.nuevaCanchaOpened = false;
	}
	protected onNuevaCanchaButtonClicked() {
		this.nuevaCanchaOpened = true;
	}
}
