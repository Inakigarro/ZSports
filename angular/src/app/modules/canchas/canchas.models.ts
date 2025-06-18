export const establecimientoId = "0ac1513a-36af-4f88-adb7-2cabb584feff"; //Id de pruebas.
export interface CrearCanchaRequest {
	numero: number;
	tipoSuelo: TipoSuelo;
	establecimientoId: string;
}

export interface Cancha {
	id: string;
	numero: number;
	tipoSuelo: TipoSuelo;
	tipoSueloParseado: string;
	establecimientoId: string;
}

export enum TipoSuelo {
	SinDefinir = 0,
	Cemento = 1,
	PolvoLadrillo = 2,
	Cesped = 3,
	SinteticoArena = 4,
	SinteticoAgua = 5,
	SinteticoCaucho = 6,
	Parquet = 7,
}

export function parseTipoSuelo(tipoSuelo: TipoSuelo): string {
	switch (tipoSuelo) {
		case TipoSuelo.SinDefinir:
			return "Sin Definir";
		case TipoSuelo.Cemento:
			return "Cemento";
		case TipoSuelo.PolvoLadrillo:
			return "Polvo Ladrillo";
		case TipoSuelo.Cesped:
			return "Césped";
		case TipoSuelo.SinteticoArena:
			return "Sintético Arena";
		case TipoSuelo.SinteticoAgua:
			return "Sintético Agua";
		case TipoSuelo.SinteticoCaucho:
			return "Sintético Caucho";
		case TipoSuelo.Parquet:
			return "Parquet";
		default:
			return "Desconocido";
	}
}
