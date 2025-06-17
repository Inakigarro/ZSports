import {
	ApplicationConfig,
	provideZoneChangeDetection,
	isDevMode,
} from "@angular/core";
import { provideRouter } from "@angular/router";

import { routes } from "./app.routes";
import { provideState, provideStore } from "@ngrx/store";
import { provideEffects } from "@ngrx/effects";
import { provideRouterStore } from "@ngrx/router-store";
import { provideStoreDevtools } from "@ngrx/store-devtools";
import {
	CANCHAS_FEATURE_KEY,
	canchasReducer,
} from "./modules/canchas/state/canchas.reducer";
import { provideHttpClient } from "@angular/common/http";
import { CanchasEffects } from "./modules/canchas/state/canchas.effects";

export const appConfig: ApplicationConfig = {
	providers: [
		provideZoneChangeDetection({ eventCoalescing: true }),
		provideHttpClient(),
		provideRouter(routes),
		provideStore(),
		provideState(CANCHAS_FEATURE_KEY, canchasReducer),
		provideEffects([CanchasEffects]),
		provideRouterStore(),
		provideStoreDevtools({ maxAge: 25, logOnly: !isDevMode() }),
	],
};
