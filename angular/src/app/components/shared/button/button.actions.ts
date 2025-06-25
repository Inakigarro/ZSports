import { createAction, props } from "@ngrx/store";

export const buttonClicked = createAction(
	"[Button] - Button clicked",
	props<{ id: string }>()
);
