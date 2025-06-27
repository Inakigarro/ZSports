import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ButtonComponent, ButtonType } from 'components';
import { ShellComponent } from './shell/shell.component';

@Component({
	selector: 'app-root',
	imports: [ShellComponent],
	templateUrl: './app.component.html',
	styleUrl: './app.component.scss',
})
export class AppComponent {
	title = 'user';
	buttonType: ButtonType = 'primary';
	testButton = {
		id: 'test-button',
		label: 'Test Button',
		type: this.buttonType,
		disabled: false,
	};

	protected onButtonClick(event: string) {
		console.log('Button clicked with Id:', event);
	}
}
