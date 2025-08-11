import { Component, OnDestroy, OnInit } from '@angular/core';
import {
	ButtonComponent,
	ListComponent,
	SideComponent,
	CardComponent,
	Button,
	ListColumn,
} from 'components';
import { Cancha, TipoSuelo } from './models';
import { CanchasService } from './canchas.service';
import { filter, Subject, takeUntil } from 'rxjs';
import { CrearEditarCanchaComponent } from './crear-editar-cancha.component.ts/crear-editar-cancha.component';

const establecimientoId: string = '7A88D6F3-4776-4C35-A644-3DA57957C486';

@Component({
	selector: 'admin-canchas',
	templateUrl: './canchas.component.html',
	styleUrl: './canchas.component.scss',
	standalone: true,
	imports: [
		ListComponent,
		ButtonComponent,
		SideComponent,
		CardComponent,
		CrearEditarCanchaComponent,
	],
})
export class CanchasComponent implements OnInit, OnDestroy {
	private destroy$ = new Subject<void>();
	protected id: string = 'canchas';
	protected title: string = 'Canchas';
	protected sidePanelOpened: boolean = false;
	protected isEdition: boolean = false;
	protected nuevaCanchaButton: Button = {
		id: 'nuevaCancha',
		label: 'Nueva Cancha',
		icon: 'fa-solid fa-plus',
		iconPosition: 'left',
		type: 'success',
		hideLabelOnMobile: true,
		disabled: false,
	};

	protected canchasLoaded: boolean = false;
	protected columns: ListColumn<Cancha>[] = [
		{
			key: 'numero',
			label: 'N°',
			align: 'center',
			width: '5%',
		},
		{
			key: 'tipoSueloParseado',
			label: 'Tipo de Suelo',
			align: 'left',
			width: '85%',
		},
	];
	protected canchas: Cancha[] = [];
	protected currentCancha: Cancha | undefined;

	constructor(private service: CanchasService) {}

	ngOnInit(): void {
		this.service
			.cargarCanchasPorEstablecimiento(establecimientoId)
			.pipe(
				takeUntil(this.destroy$),
				filter((canchas) => !!canchas)
			)
			.subscribe({
				next: (canchas) => {
					this.canchas = canchas;
					this.canchasLoaded = true;
				},
				error: (error) => {
					console.error('Error loading canchas:', error);
					this.canchas = [];
					this.canchasLoaded = true;
				},
			});
	}

	ngOnDestroy(): void {
		this.destroy$.next();
		this.destroy$.complete();
		this.canchas = [];
	}

	protected onNuevaCancha() {
		this.isEdition = false;
		this.currentCancha = undefined;
		this.sidePanelOpened = true;
	}
	protected onEdit(canchaId: string) {
		this.isEdition = true;
		this.currentCancha = this.canchas.find((c) => c.id === canchaId);
		this.sidePanelOpened = true;
	}
	protected onDelete(canchaId: string) {
		console.log(`Delete cancha with ID: ${canchaId}`);
	}

	protected sidePanelClosed() {
		this.sidePanelOpened = false;
		this.isEdition = false;
		this.currentCancha = undefined;
	}
}
