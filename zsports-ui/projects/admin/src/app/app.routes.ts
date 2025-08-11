import { Routes } from '@angular/router';

export const routes: Routes = [
	{
		path: '',
		title: 'Inicio',
		loadComponent: () =>
			import('./home/home.component').then((c) => c.HomeComponent),
	},
	{
		path: 'canchas',
		title: 'Canchas',
		loadComponent: () =>
			import('./canchas/canchas.component').then((c) => c.CanchasComponent),
	},
];
