import { Component, input, OnDestroy, OnInit, output } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { CanchasService } from '../canchas.service';
import {
	Cancha,
	CrearCanchaRequest,
	EditarCanchaRequest,
	establecimientoId,
	parseTipoSuelo,
	TipoSuelo,
} from '../models';
import {
	CardComponent,
	ButtonComponent,
	Button,
	LabelComponent,
	SelectOption,
	SelectComponent,
} from 'components';
import {
	FormBuilder,
	FormControl,
	FormGroup,
	FormsModule,
	ReactiveFormsModule,
	Validators,
} from '@angular/forms';

@Component({
	selector: 'zs-crear-editar-cancha',
	standalone: true,
	templateUrl: './crear-editar-cancha.component.html',
	styleUrl: './crear-editar-cancha.component.scss',
	providers: [],
	imports: [
		CardComponent,
		ButtonComponent,
		FormsModule,
		ReactiveFormsModule,
		LabelComponent,
		SelectComponent,
	],
})
export class CrearEditarCanchaComponent implements OnInit, OnDestroy {
	private destroy$ = new Subject<void>();
	protected id: string = 'nueva-cancha-modal';
	protected form: FormGroup;
	protected suelosOpciones: SelectOption[] = [];

	protected guardarButton: Button = {
		id: 'guardar-cancha-button',
		label: 'Guardar',
		type: 'primary',
		icon: 'fa-solid fa-check',
	};
	protected cancelarButton: Button = {
		id: 'cancelar-cancha-button',
		label: 'Cancelar',
		type: 'danger',
		icon: 'fa-solid fa-xmark',
	};

	public isEdition = input<boolean>(false);
	public currentCancha = input<Cancha>();
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
			.filter((value) => typeof value === 'number')
			.map((tiposSuelo) => ({
				label: parseTipoSuelo(tiposSuelo),
				value: tiposSuelo,
			}));

		if (this.isEdition() && this.currentCancha()) {
			this.form.patchValue({
				numero: this.currentCancha()!.numero,
				tipoSuelo: this.currentCancha()!.tipoSuelo,
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
					id: this.currentCancha()!.id,
					numero: this.form.controls['numero'].value,
					tipoSuelo: Number.parseInt(this.form.controls['tipoSuelo'].value),
					establecimientoId: establecimientoId,
				};

				this.service
					.editarCancha(request)
					.pipe(takeUntil(this.destroy$))
					.subscribe({
						next: (cancha) => {
							this.onClose.emit();
							this.form.reset();
						},
						error: (error) => {
							console.error('Error editing cancha:', error);
						},
					});
			} else {
				const request: CrearCanchaRequest = {
					numero: this.form.controls['numero'].value,
					tipoSuelo: Number.parseInt(this.form.controls['tipoSuelo'].value),
					establecimientoId: establecimientoId,
				};

				this.service
					.agregarCancha(request)
					.pipe(takeUntil(this.destroy$))
					.subscribe({
						next: (cancha) => {
							this.onClose.emit();
							this.form.reset();
						},
						error: (error) => {
							console.error('Error adding cancha:', error);
						},
					});
			}
		}
	}

	public onCancel() {
		this.form.reset();
		this.onClose.emit();
	}
}
