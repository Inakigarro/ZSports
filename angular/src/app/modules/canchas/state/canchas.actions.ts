import { createActionGroup, emptyProps, props } from "@ngrx/store";
import { Cancha, CrearCanchaRequest } from "../canchas.models";

export const CanchasActions = createActionGroup({
	source: "Canchas",
	events: {
		"Iniciar Carga de Canchas": emptyProps(),
		"Canchas Cargadas": props<{ canchas: Cancha[] }>(),
		"Editar Cancha": props<{ canchaId: string }>(),
		"Cancha Cargada": props<{ cancha: Cancha }>(),
		"Crear Cancha": props<{ cancha: CrearCanchaRequest }>(),
		"Cancha Creada": emptyProps(),
	},
});
