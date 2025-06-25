import { CommonModule } from "@angular/common";
import { Component, Input } from "@angular/core";

export type NavbarOrientation = "horizontal" | "vertical";

@Component({
	selector: "app-navbar",
	standalone: true,
	templateUrl: "./navbar.component.html",
	styleUrl: "./navbar.component.scss",
	imports: [CommonModule],
})
export class NavbarComponent {
	@Input() orientation: NavbarOrientation = "horizontal";
}
