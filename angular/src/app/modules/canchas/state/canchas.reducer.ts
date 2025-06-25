import { createReducer, on } from "@ngrx/store";
import { Cancha } from "../canchas.models";
import { CanchasActions } from "./canchas.actions";

export const CANCHAS_FEATURE_KEY = "canchas";

export interface CanchasState {
	canchas: Cancha[];
	loading: boolean;
	error?: string;
	currentCancha?: Cancha;
	crearCanchaSucceded?: boolean;
	editarCanchaSucceded?: boolean;
}

export const initialCanchasState: CanchasState = {
	canchas: [],
	loading: false,
};

export const canchasReducer = createReducer(
	initialCanchasState,
	on(CanchasActions.iniciarCargaDeCanchas, (state) => ({
		...state,
		canchas: [],
		loading: true,
		error: undefined,
	})),
	on(CanchasActions.canchasCargadas, (state, action) => ({
		...state,
		canchas: action.canchas,
		loading: false,
	})),
	on(CanchasActions.editarCancha, (state) => ({
		...state,
		loading: true,
		currentCancha: undefined,
	})),
	on(CanchasActions.canchaCargada, (state, action) => ({
		...state,
		currentCancha: action.cancha,
		loading: false,
	})),
	on(CanchasActions.crearCancha, (state) => ({
		...state,
		loading: true,
		crearCanchaSucceded: false,
	})),
	on(CanchasActions.canchaCreada, (state) => ({
		...state,
		loading: false,
		crearCanchaSucceded: true,
	})),
	on(CanchasActions.editarCancha, (state) => ({
		...state,
		loading: true,
		editarCanchaSucceded: false,
	})),
	on(CanchasActions.canchaEditada, (state) => ({
		...state,
		loading: false,
		editarCanchaSucceded: true,
	})),
	on(CanchasActions.eliminarCancha, (state) => ({
		...state,
		loading: true,
	})),
	on(CanchasActions.canchaEliminada, (state) => ({
		...state,
		loading: false,
	}))
);
