import { Routes } from '@angular/router';

export const routes: Routes = [
	{
		path: 'canchas',
		title: 'Canchas',
		loadComponent: () =>
			import('./canchas/canchas.component').then((c) => c.CanchasComponent),
	},
	{
		path: '',
		redirectTo: 'canchas',
		pathMatch: 'full',
	},
];
