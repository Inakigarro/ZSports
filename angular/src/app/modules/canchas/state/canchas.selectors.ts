import { createFeatureSelector, createSelector } from "@ngrx/store";
import { CANCHAS_FEATURE_KEY, CanchasState } from "./canchas.reducer";

const state = createFeatureSelector<CanchasState>(CANCHAS_FEATURE_KEY);

export const selectLoading = createSelector(state, (state) => state.loading);

export const selectCanchas = createSelector(state, (state) => state.canchas);

export const selectCurrentCancha = createSelector(
	state,
	(state) => state.currentCancha
);

export const selectError = createSelector(state, (state) => state.error);

export const selectCrearCanchaSucceded = createSelector(
	state,
	(state) => state.crearCanchaSucceded
);
