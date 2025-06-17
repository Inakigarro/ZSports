import { Routes } from "@angular/router";

export const routes: Routes = [
	{
		path: "",
		loadComponent: () =>
			import("@modules/home/home.component").then((c) => c.HomeComponent),
		pathMatch: "full",
	},
	{
		path: "reservas",
		loadComponent: () =>
			import("@modules/reservas/reservas-list.component").then(
				(m) => m.ReservasListComponent
			),
	},
	{
		path: "canchas",
		loadComponent: () =>
			import("@modules/canchas/canchas-list.component").then(
				(m) => m.CanchasListComponent
			),
	},
];
