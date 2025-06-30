import { Component, input } from '@angular/core';

@Component({
	selector: 'zs-card',
	templateUrl: './card.component.html',
	styleUrl: './card.component.scss',
	standalone: true,
	imports: [],
})
export class CardComponent {
	public id = input.required<string>();
}
