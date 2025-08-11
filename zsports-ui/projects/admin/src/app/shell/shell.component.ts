import { Component, input, OnInit } from '@angular/core';
import { ButtonComponent, Button } from 'components';
import {
	ActivatedRoute,
	Router,
	RouterOutlet,
	RouterLink,
	RouterLinkActive,
} from '@angular/router';
import { routes } from '../app.routes';
import { CommonModule } from '@angular/common';

@Component({
	selector: 'admin-shell',
	templateUrl: './shell.component.html',
	styleUrl: './shell.component.scss',
	standalone: true,
	imports: [
		ButtonComponent,
		RouterOutlet,
		RouterLink,
		RouterLinkActive,
		CommonModule,
	],
})
export class ShellComponent implements OnInit {
	// Inputs.
	title = input<string>('Inicio');

	// Navigation routes
	navigationRoutes: Array<{ path: string; title: string }> = [];

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

	constructor(private router: Router) {}

	ngOnInit() {
		// Extract navigation routes from app routes
		this.navigationRoutes = routes.map((route) => ({
			path: `/${route.path}`,
			title: route.title as string,
		}));
	}

	protected onButtonClick(event: string) {
		console.log('Button clicked with Id:', event);
	}
}
