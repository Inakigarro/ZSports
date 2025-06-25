import { Component, Input, OnDestroy, OnInit } from "@angular/core";
import { Router, RouterLink, RouterLinkActive } from "@angular/router";

@Component({
	selector: "app-nav-item",
	standalone: true,
	templateUrl: "./nav-item.component.html",
	styleUrl: "./nav-item.component.scss",
	imports: [RouterLink, RouterLinkActive],
})
export class NavItemComponent implements OnInit, OnDestroy {
	@Input() label: string = "";
	@Input() route: string | any[] = "";
	@Input() icon?: string;
	@Input() hideLabelOnMobile: boolean = false;

	protected isMobile: boolean = false;

	private resizeListener = () => {
		this.isMobile = window.matchMedia("(max-width: 600px)").matches;
	};

	ngOnInit() {
		this.resizeListener();
		window.addEventListener("resize", this.resizeListener);
	}

	ngOnDestroy() {
		window.removeEventListener("resize", this.resizeListener);
	}
}
