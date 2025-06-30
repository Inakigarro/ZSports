import { CommonModule } from '@angular/common';
import { Component, input, output } from '@angular/core';
import { ButtonComponent, Button } from '../button/button.component';

export type ListColumn<TItem> = {
	key: keyof TItem;
	label: string;
	align?: 'left' | 'center' | 'right';
	width?: string;
};

@Component({
	selector: 'zs-list',
	templateUrl: './list.component.html',
	styleUrl: './list.component.scss',
	standalone: true,
	imports: [CommonModule, ButtonComponent],
})
export class ListComponent<TItem extends { id: string }> {
	// Inputs.
	id = input.required<string>();
	columns = input.required<ListColumn<TItem>[]>();
	items = input.required<TItem[]>();

	// Outputs.
	onEdit = output<string>();
	onDelete = output<string>();

	// Buttons.
	editarButton: Button = {
		id: 'edit-button',
		icon: 'fa-solid fa-pencil',
		type: 'success',
		iconPosition: 'left',
		hideLabelOnMobile: true,
		disabled: false,
	};

	eliminarButton: Button = {
		id: 'delete-button',
		icon: 'fa-solid fa-trash',
		type: 'danger',
		iconPosition: 'left',
		hideLabelOnMobile: true,
		disabled: false,
	};
}
