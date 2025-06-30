import { Component, input, output } from '@angular/core';

@Component({
	selector: 'zs-side',
	standalone: true,
	templateUrl: './side.component.html',
	styleUrl: './side.component.scss',
	imports: [],
})
export class SideComponent {
	expanded = input<boolean>(false);
	closeOnBackdrop = input<boolean>(true);
	closed = output<void>();

	onBackdropClick() {
		if (this.closeOnBackdrop()) {
			this.closed.emit();
		}
	}
}
