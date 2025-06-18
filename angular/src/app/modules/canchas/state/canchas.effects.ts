import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { CanchasService } from "../canchas.service";
import { CanchasActions } from "./canchas.actions";
import { filter, map, switchMap } from "rxjs";
import { editRowAction } from "@app/components/shared/list/list.actions";
import { establecimientoId } from "../canchas.models";

@Injectable()
export class CanchasEffects {
	public initCanchas$ = createEffect(() =>
		this.actions.pipe(
			ofType(
				CanchasActions.iniciarCargaDeCanchas,
				CanchasActions.canchaCreada),
			switchMap(() =>
				this.service.cargarCanchasPorEstablecimiento(establecimientoId).pipe(
					filter((x) => !!x),
					map((canchas) => CanchasActions.canchasCargadas({ canchas }))
				)
			)
		)
	);

	public crearCancha$ = createEffect(() =>
		this.actions.pipe(
			ofType(CanchasActions.crearCancha),
			switchMap((action) =>
				this.service.agregarCancha(action.cancha).pipe(
					map(() => CanchasActions.canchaCreada())
				)
			)
		)
	);

	public editCanchaRequest$ = createEffect(() =>
		this.actions.pipe(
			ofType(editRowAction),
			filter((action) => action.id === "canchas"),
			map((action) => CanchasActions.editarCancha({ canchaId: action.rowId }))
		)
	);

	public cargarCancha$ = createEffect(() =>
		this.actions.pipe(
			ofType(CanchasActions.editarCancha),
			switchMap((action) =>
				this.service.obtenerCanchaPorId(action.canchaId).pipe(
					filter((x) => !!x),
					map((cancha) => CanchasActions.canchaCargada({ cancha }))
				)
			)
		)
	);
	constructor(
		private readonly actions: Actions,
		private readonly service: CanchasService
	) {}
}
