import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Cancha, CrearCanchaRequest, EditarCanchaRequest } from './models';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class CanchasService {
	private url: string = environment.rootUrl + '/canchas';

	constructor(private readonly httpClient: HttpClient) {}

	public agregarCancha(crearCanchaRequest: CrearCanchaRequest) {
		return this.httpClient.post<Cancha>(
			`${this.url}/agregarCancha`,
			crearCanchaRequest
		);
	}

	public cargarCanchasPorEstablecimiento(establecimientoId: string) {
		let params = new HttpParams();
		params = params.set('establecimientoId', establecimientoId);
		return this.httpClient.get<Cancha[]>(
			`${this.url}/obtenerCanchasPorEstablecimiento`,
			{ params }
		);
	}

	public obtenerCanchaPorId(id: string) {
		let params = new HttpParams();
		params = params.set('canchaId', id);
		return this.httpClient.get<Cancha>(`${this.url}/obtenerCanchaPorId`, {
			params,
		});
	}

	public editarCancha(editarCanchaRequest: EditarCanchaRequest) {
		return this.httpClient.put<Cancha>(
			`${this.url}/editarCancha`,
			editarCanchaRequest
		);
	}

	public eliminarCancha(id: string) {
		let params = new HttpParams();
		params = params.set('canchaId', id);
		return this.httpClient.delete(`${this.url}/eliminarCancha`, {
			params,
		});
	}
}
