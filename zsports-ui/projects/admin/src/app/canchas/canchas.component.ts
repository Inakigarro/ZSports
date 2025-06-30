import { Component } from '@angular/core';
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

@Component({
	selector: 'admin-canchas',
	templateUrl: './canchas.component.html',
	styleUrl: './canchas.component.scss',
	standalone: true,
	imports: [ListComponent, ButtonComponent, SideComponent, CardComponent],
})
export class CanchasComponent {
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

	constructor(service: CanchasService) {}

	protected onEdit(canchaId: string) {
		this.isEdition = true;
		this.sidePanelOpened = true;
	}
	protected onDelete(canchaId: string) {
		console.log(`Delete cancha with ID: ${canchaId}`);
	}
}
