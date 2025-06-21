import { Component, input, OnDestroy, OnInit, output } from "@angular/core";
import { Subject, takeUntil } from "rxjs";
import { CanchasService } from "../../canchas.service";
import {
	Cancha,
	CrearCanchaRequest,
	EditarCanchaRequest,
	establecimientoId,
	parseTipoSuelo,
	TipoSuelo,
} from "../../canchas.models";
import { CardComponent } from "@components/card/card.component";
import { ButtonComponent } from "@components/button/button.component";
import {
	FormBuilder,
	FormControl,
	FormGroup,
	FormsModule,
	ReactiveFormsModule,
	Validators,
} from "@angular/forms";
import { LabelComponent } from "@components/form/label/label.component";
import { InputComponent } from "@app/components/form/input/input.component";
import {
	SelectComponent,
	SelectOption,
} from "@components/form/select/select.component";
import { Button } from "@app/components/shared/button/button.models";
import { CanchasActions } from "../../state/canchas.actions";

@Component({
	selector: "app-crear-editar-cancha",
	standalone: true,
	templateUrl: "./crear-editar-cancha.component.html",
	styleUrl: "./crear-editar-cancha.component.scss",
	providers: [],
	imports: [
		CardComponent,
		ButtonComponent,
		FormsModule,
		ReactiveFormsModule,
		LabelComponent,
		InputComponent,
		SelectComponent,
	],
})
export class CrearEditarCanchaComponent implements OnInit, OnDestroy {
	private destroy$ = new Subject<void>();
	private currentCancha$ = this.service.canchaActual$;
	private canchaActual: Cancha;
	protected id: string = "nueva-cancha-modal";
	protected form: FormGroup;
	protected suelosOpciones: SelectOption[] = [];
	protected guardarButton: Button = {
		id: "guardar-cancha-button",
		label: "Guardar",
		type: "primary",
		icon: "fa-solid fa-check",
	};
	protected cancelarButton: Button = {
		id: "cancelar-cancha-button",
		label: "Cancelar",
		type: "danger",
		icon: "fa-solid fa-xmark",
	};

	public isEdition = input<boolean>(false);
	public onClose = output<void>();
	constructor(
		private readonly service: CanchasService,
		private readonly formBuilder: FormBuilder
	) {}

	ngOnInit() {
		this.form = this.formBuilder.group({
			numero: new FormControl<number>(0, [
				Validators.required,
				Validators.min(1),
			]),
			tipoSuelo: new FormControl<number>(0, [
				Validators.required,
				Validators.min(1),
			]),
		});

		this.suelosOpciones = Object.values(TipoSuelo)
			.filter((value) => typeof value === "number")
			.map((tiposSuelo) => ({
				label: parseTipoSuelo(tiposSuelo),
				value: tiposSuelo,
			}));

		if (this.isEdition()) {
			this.currentCancha$.subscribe((cancha) => {
				if (cancha) {
					this.canchaActual = cancha;
					this.form.patchValue({
						numero: cancha.numero,
						tipoSuelo: cancha.tipoSuelo,
					});
				}
			});
		}
	}

	ngOnDestroy() {
		// Cleanup logic here
		this.destroy$.next();
		this.destroy$.complete();
	}

	public onSubmit() {
		if (this.form.valid) {
			if (this.isEdition()) {
				const request: EditarCanchaRequest = {
					id: this.canchaActual.id,
					numero: this.form.controls["numero"].value,
					tipoSuelo: Number.parseInt(this.form.controls["tipoSuelo"].value),
					establecimientoId: establecimientoId,
				};

				this.service.dispatch(CanchasActions.editarCancha({ cancha: request }));
				this.service.editarCanchaSucceded$
					.pipe(takeUntil(this.destroy$))
					.subscribe((success) => {
						if (success) {
							this.onClose.emit();
							this.form.reset();
						}
					});
			} else {
				const request: CrearCanchaRequest = {
					numero: this.form.controls["numero"].value,
					tipoSuelo: Number.parseInt(this.form.controls["tipoSuelo"].value),
					establecimientoId: establecimientoId,
				};

				this.service.dispatch(CanchasActions.crearCancha({ cancha: request }));
				this.service.crearCanchaSucceded$
					.pipe(takeUntil(this.destroy$))
					.subscribe((success) => {
						if (success) {
							this.onClose.emit();
							this.form.reset();
						}
					});
			}
		}
	}

	public onCancel() {
		this.form.reset();
		this.onClose.emit();
	}
}
