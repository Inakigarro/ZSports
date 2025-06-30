import { Component, OnInit } from '@angular/core';
import { ButtonType } from 'components';
import { ShellComponent } from './shell/shell.component';
import {
	ActivatedRoute,
	ActivatedRouteSnapshot,
	NavigationEnd,
	Router,
} from '@angular/router';
import { Title } from '@angular/platform-browser';
import { filter } from 'rxjs';

@Component({
	selector: 'app-root',
	imports: [ShellComponent],
	templateUrl: './app.component.html',
	styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
	title = '';

	constructor(private readonly router: Router) {}

	ngOnInit(): void {
		this.router.events
			.pipe(filter((event) => event instanceof NavigationEnd))
			.subscribe((event) => {
				let currentRoute: ActivatedRouteSnapshot | null =
					this.router.routerState.snapshot.root;
				let routeTitle = '';
				while (currentRoute) {
					if (currentRoute.routeConfig && currentRoute.routeConfig.title) {
						routeTitle = currentRoute.routeConfig.title as string;
						break;
					}
					currentRoute = currentRoute.firstChild;
				}
				this.title = routeTitle || 'ZSports';
			});
	}
	protected onButtonClick(event: string) {
		console.log('Button clicked with Id:', event);
	}
}
