import { Injectable } from "@angular/core";
import { Action, Store } from "@ngrx/store";
import { environment } from "@app/environments/environment.local";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Cancha } from "./canchas.models";
import {
	selectCanchas,
	selectCrearCanchaSucceded,
	selectCurrentCancha,
	selectLoading,
} from "./state/canchas.selectors";

@Injectable({ providedIn: "root" })
export class CanchasService {
	private url: string = environment.rootUrl + "/canchas";
	public canchasLoading$ = this.store.select(selectLoading);
	public canchas$ = this.store.select(selectCanchas);
	public canchaActual$ = this.store.select(selectCurrentCancha);
	public crearCanchaSucceded$ = this.store.select(selectCrearCanchaSucceded);

	constructor(
		private readonly store: Store,
		private readonly httpClient: HttpClient
	) {}

	public dispatch(action: Action) {
		this.store.dispatch(action);
	}

	public cargarCanchasPorEstablecimiento(establecimientoId: string) {
		let params = new HttpParams();
		params = params.set("establecimientoId", establecimientoId);
		return this.httpClient.get<Cancha[]>(
			`${this.url}/obtenerCanchasPorEstablecimiento`,
			{ params }
		);
	}

	public obtenerCanchaPorId(id: string) {
		let params = new HttpParams();
		params = params.set("canchaId", id);
		return this.httpClient.get<Cancha>(`${this.url}/obtenerCanchaPorId`, {
			params,
		});
	}
}
