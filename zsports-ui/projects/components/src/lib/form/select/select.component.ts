import { Component, Input, forwardRef, input } from '@angular/core';
import {
	NG_VALUE_ACCESSOR,
	ControlValueAccessor,
	FormsModule,
	ReactiveFormsModule,
} from '@angular/forms';

export type SelectOption = { label: string; value: string | number };

@Component({
	selector: 'zs-select',
	standalone: true,
	templateUrl: './select.component.html',
	styleUrl: './select.component.scss',
	imports: [FormsModule, ReactiveFormsModule],
	providers: [
		{
			provide: NG_VALUE_ACCESSOR,
			useExisting: forwardRef(() => SelectComponent),
			multi: true,
		},
	],
})
export class SelectComponent implements ControlValueAccessor {
	id = input.required<string>();
	@Input() options: SelectOption[] = [];
	@Input() disabled = false;
	@Input() placeholder = '';
	@Input() required = false;

	value: string | number = '';

	private onChange = (value: any) => {};
	private onTouched = () => {};

	writeValue(value: any): void {
		this.value = value;
	}

	registerOnChange(fn: any): void {
		this.onChange = fn;
	}

	registerOnTouched(fn: any): void {
		this.onTouched = fn;
	}

	setDisabledState(isDisabled: boolean): void {
		this.disabled = isDisabled;
	}

	onSelect(event: Event) {
		const select = event.target as HTMLSelectElement;
		this.value = select.value;
		this.onChange(this.value);
	}

	onBlur() {
		this.onTouched();
	}
}
