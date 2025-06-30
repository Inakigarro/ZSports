import { Component } from '@angular/core';
import { SideComponent, ButtonComponent, Button } from 'components';

@Component({
	selector: 'user-home',
	templateUrl: './home.component.html',
	styleUrl: './home.component.scss',
	standalone: true,
	imports: [SideComponent, ButtonComponent],
})
export class HomeComponent {
	protected expanded: boolean = false;
	protected openSidePanelButton: Button = {
		id: 'open-side-panel',
		label: 'Abrir panel',
		type: 'success',
		disabled: false,
		hideLabelOnMobile: false,
	};
	protected closeSidePanelButton: Button = {
		id: 'close-side-panel',
		label: 'Cerrar panel',
		type: 'danger',
		disabled: false,
		hideLabelOnMobile: false,
	};

	protected toggleSidePanel() {
		this.expanded = !this.expanded;
	}
}
