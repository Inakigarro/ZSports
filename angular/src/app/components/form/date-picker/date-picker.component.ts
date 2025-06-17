import { Component, forwardRef, Input } from "@angular/core";
import {
	NG_VALUE_ACCESSOR,
	ControlValueAccessor,
	FormsModule,
	ReactiveFormsModule,
} from "@angular/forms";

@Component({
	selector: "app-date-picker",
	standalone: true,
	templateUrl: "./date-picker.component.html",
	styleUrl: "./date-picker.component.scss",
	imports: [FormsModule, ReactiveFormsModule],
	providers: [
		{
			provide: NG_VALUE_ACCESSOR,
			useExisting: forwardRef(() => DatePickerComponent),
			multi: true,
		},
	],
})
export class DatePickerComponent implements ControlValueAccessor {
	@Input() id: string = "";
	@Input() placeholder: string = "";
	@Input() disabled: boolean = false;
	@Input() required: boolean = false;

	value: string = "";

	onChange = (value: string) => {};
	onTouched = () => {};

	writeValue(value: string): void {
		this.value = value || "";
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

	onInput(event: Event) {
		const input = event.target as HTMLInputElement;
		this.value = input.value;
		this.onChange(this.value);
	}
}
