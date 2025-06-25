import { Component, Input, input, output } from "@angular/core";
import { createAction, props } from "@ngrx/store";
import { ListService } from "./list.service";
import { Button } from "../shared/button/button.models";
import { ButtonComponent } from "../button/button.component";
import { deleteRowAction, editRowAction } from "../shared/list/list.actions";
import { Entity } from "../shared/entity";

export type ListColumn<TItem> = {
	key: keyof TItem;
	label: string;
	width?: string;
	align?: "left" | "center" | "right";
};

@Component({
	selector: "app-list",
	standalone: true,
	templateUrl: "./list.component.html",
	styleUrl: "./list.component.scss",
	providers: [],
	imports: [ButtonComponent],
})
export class ListComponent<TItem extends Entity> {
	id = input.required<string>();
	columns = input.required<ListColumn<TItem>[]>();
	data = input.required<TItem[]>();
	onEdit = output<string>();
	onDelete = output<string>();

	editarButton: Button = {
		id: "editar-button",
		icon: "fa-solid fa-pen-to-square",
		type: "secondary",
	};

	eliminarButton: Button = {
		id: "eliminar-button",
		icon: "fa-solid fa-trash",
		type: "danger",
	};

	constructor(private readonly service: ListService) {}

	protected onEditRowButtonClicked(rowId: string) {
		this.service.dispatch(editRowAction({ id: this.id(), rowId: rowId }));
		this.onEdit.emit(rowId);
	}

	protected onDeleteRowButtonClicked(rowId: string) {
		this.service.dispatch(deleteRowAction({ id: this.id(), rowId: rowId }));
		this.onDelete.emit(rowId);
	}
}
