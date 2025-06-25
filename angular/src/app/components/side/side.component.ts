import { Component, Input, Output, EventEmitter } from "@angular/core";

@Component({
	selector: "app-side",
	standalone: true,
	templateUrl: "./side.component.html",
	styleUrl: "./side.component.scss",
	imports: [],
})
export class SideComponent {
	@Input() expanded: boolean = false;
	@Input() closeOnBackdrop: boolean = true;
	@Output() closed = new EventEmitter<void>();

	onBackdropClick() {
		if (this.closeOnBackdrop) {
			this.closed.emit();
		}
	}
}
