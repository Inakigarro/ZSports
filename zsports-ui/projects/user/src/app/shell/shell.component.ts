import { Component, input } from '@angular/core';
import { ButtonComponent, Button } from 'components';

@Component({
	selector: 'zs-shell',
	templateUrl: './shell.component.html',
	styleUrl: './shell.component.scss',
	standalone: true,
	imports: [ButtonComponent],
})
export class ShellComponent {
	// Inputs.
	title = input<string>('Establecimiento');

	mainButton: Button = {
		id: 'home-button',
		label: 'Inicio',
		icon: 'fa-solid fa-home',
		type: 'primary',
		hideLabelOnMobile: true,
		iconPosition: 'left',
		disabled: false,
	};

	userButton: Button = {
		id: 'user-button',
		label: 'Perfil',
		icon: 'fa-solid fa-user',
		type: 'secondary',
		hideLabelOnMobile: true,
		iconPosition: 'left',
		disabled: false,
	};

	protected onButtonClick(event: string) {
		console.log('Button clicked with Id:', event);
	}
}
