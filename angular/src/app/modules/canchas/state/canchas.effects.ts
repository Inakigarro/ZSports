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
				CanchasActions.canchaCreada,
				CanchasActions.canchaEditada,
				CanchasActions.canchaEliminada
			),
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
				this.service
					.agregarCancha(action.cancha)
					.pipe(map(() => CanchasActions.canchaCreada()))
			)
		)
	);

	public editCanchaRequest$ = createEffect(() =>
		this.actions.pipe(
			ofType(editRowAction),
			filter((action) => action.id === "canchas"),
			map((action) => CanchasActions.buscarCancha({ canchaId: action.rowId }))
		)
	);

	public cargarCancha$ = createEffect(() =>
		this.actions.pipe(
			ofType(CanchasActions.buscarCancha),
			switchMap((action) =>
				this.service.obtenerCanchaPorId(action.canchaId).pipe(
					filter((x) => !!x),
					map((cancha) => CanchasActions.canchaCargada({ cancha }))
				)
			)
		)
	);

	public editarCancha$ = createEffect(() =>
		this.actions.pipe(
			ofType(CanchasActions.editarCancha),
			switchMap((action) =>
				this.service.editarCancha(action.cancha).pipe(
					filter((x) => !!x),
					map(() => CanchasActions.canchaEditada())
				)
			)
		)
	);

	public eliminarCancha$ = createEffect(() =>
		this.actions.pipe(
			ofType(CanchasActions.eliminarCancha),
			switchMap((action) =>
				this.service.eliminarCancha(action.canchaId).pipe(
					filter((x) => !!x),
					map(() => CanchasActions.canchaEliminada())
				)
			)
		)
	);

	constructor(
		private readonly actions: Actions,
		private readonly service: CanchasService
	) {}
}
