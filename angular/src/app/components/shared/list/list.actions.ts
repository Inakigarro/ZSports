import { createAction, props } from "@ngrx/store";

export const editRowAction = createAction(
	"[List] - Editar fila",
	props<{ id: string; rowId: string }>()
);
export const deleteRowAction = createAction(
	"[List] - Eliminar fila",
	props<{ id: string; rowId: string }>()
);
