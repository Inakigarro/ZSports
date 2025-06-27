import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
	selector: 'zs-button',
	standalone: true,
	templateUrl: './button.component.html',
	styleUrl: './button.component.scss',
	imports: [CommonModule],
})
export class ButtonComponent {}
