import { CommonModule } from '@angular/common';
import { Component, Input, input } from '@angular/core';
import {
	ControlValueAccessor,
	FormsModule,
	ReactiveFormsModule,
	NG_VALUE_ACCESSOR,
	NG_VALIDATORS,
	Validator,
	AbstractControl,
	ValidationErrors,
} from '@angular/forms';

export type InputType = 'text' | 'number' | 'email' | 'password' | 'tel';
export type InputValueType = string | number;

@Component({
	selector: 'zs-input',
	templateUrl: './input.component.html',
	styleUrl: './input.component.scss',
	standalone: true,
	imports: [CommonModule, FormsModule, ReactiveFormsModule],
	providers: [
		{
			provide: NG_VALUE_ACCESSOR,
			useExisting: InputComponent,
			multi: true,
		},
		{
			provide: NG_VALIDATORS,
			useExisting: InputComponent,
			multi: true,
		},
	],
})
export class InputComponent implements ControlValueAccessor, Validator {
	public id = input.required<string>();
	public type = input<InputType>('text');
	public placeholder = input<string>('');
	public required = input<boolean>(false);
	public minLength = input<number>();
	public maxLength = input<number>();
	public min = input<number>();
	public max = input<number>();

	@Input()
	public disabled: boolean = false;

	protected value: InputValueType | undefined = '';
	protected isFocused: boolean = false;
	protected isTouched: boolean = false;

	private onChange: (value: InputValueType | undefined) => void = () => {};
	private onTouched: () => void = () => {};

	// ControlValueAccessor methods
	writeValue(value: InputValueType): void {
		this.value = value;
	}

	registerOnChange(fn: (value: InputValueType | undefined) => void): void {
		this.onChange = fn;
	}

	registerOnTouched(fn: () => void): void {
		this.onTouched = fn;
	}

	setDisabledState?(isDisabled: boolean): void {
		this.disabled = isDisabled;
	}

	// Validator interface
	validate(control: AbstractControl): ValidationErrors | null {
		const value = control.value;
		const errors: ValidationErrors = {};

		// Required validation
		if (this.required() && (!value || value.toString().trim() === '')) {
			errors['required'] = true;
		}

		// Email validation
		if (this.type() === 'email' && value) {
			const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
			if (!emailRegex.test(value)) {
				errors['email'] = true;
			}
		}

		// Telefono validation
		if (this.type() === 'tel' && value) {
			const phoneRegex = /^[0-9]{10}$/;
			if (!phoneRegex.test(value.toString().replace(/\s/g, ''))) {
				errors['telefono'] = true;
			}
		}

		// MinLength validation
		if (
			this.minLength() &&
			value &&
			value.toString().length < this.minLength()!
		) {
			errors['minlength'] = {
				requiredLength: this.minLength(),
				actualLength: value.toString().length,
			};
		}

		// MaxLength validation
		if (
			this.maxLength() &&
			value &&
			value.toString().length > this.maxLength()!
		) {
			errors['maxlength'] = {
				requiredLength: this.maxLength(),
				actualLength: value.toString().length,
			};
		}

		// Number validations
		if (this.type() === 'number' && value !== undefined && value !== '') {
			const numValue = Number(value);
			if (isNaN(numValue)) {
				errors['number'] = true;
			} else {
				if (this.min() !== undefined && numValue < this.min()!) {
					errors['min'] = { min: this.min(), actual: numValue };
				}
				if (this.max() !== undefined && numValue > this.max()!) {
					errors['max'] = { max: this.max(), actual: numValue };
				}
			}
		}

		return Object.keys(errors).length > 0 ? errors : null;
	}

	// Event handlers
	onInput(event: Event) {
		const input = event.target as HTMLInputElement;
		let value: InputValueType | undefined = input.value;

		if (this.type() === 'number') {
			value = value === '' ? undefined : Number(value);
		}

		this.value = value;
		this.onChange(value);
	}

	onFocus() {
		this.isFocused = true;
	}

	onBlur() {
		this.isFocused = false;
		this.isTouched = true;
		this.onTouched();
	}

	// Helper methods para el template
	get hasValue(): boolean {
		return this.value !== undefined && this.value !== '';
	}

	get inputType(): string {
		return this.type() === 'tel' ? 'tel' : this.type();
	}

	// Métodos para determinar estado visual
	get cssClasses(): string {
		const classes = ['input'];

		if (this.isFocused) classes.push('focused');
		if (this.required()) classes.push('required');
		if (this.isTouched && this.hasValue) {
			// Aquí necesitarías acceso al FormControl para verificar errores
			// Por simplicidad, asumo que tienes una forma de acceder a los errores
		}

		return classes.join(' ');
	}
}
