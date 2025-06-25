import { Component, Input, forwardRef, input } from "@angular/core";
import {
	NG_VALUE_ACCESSOR,
	ControlValueAccessor,
	FormsModule,
	ReactiveFormsModule,
} from "@angular/forms";

export type InputType = "text" | "number" | "password" | "email";
export type InputValueType = string | number;

@Component({
	selector: "app-input",
	standalone: true,
	templateUrl: "./input.component.html",
	styleUrl: "./input.component.scss",
	imports: [FormsModule, ReactiveFormsModule],
	providers: [
		{
			provide: NG_VALUE_ACCESSOR,
			useExisting: forwardRef(() => InputComponent),
			multi: true,
		},
	],
})
export class InputComponent implements ControlValueAccessor {
	id = input.required<string>();
	type = input<InputType>("text");
	placeholder = input<string>("");
	@Input() disabled = false;
	required = input<boolean>(false);

	value: InputValueType | undefined = "";

	// Métodos de ControlValueAccessor
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

	onInput(event: Event) {
		const input = event.target as HTMLInputElement;
		let value: InputValueType | undefined = input.value;
		if (this.type() === "number") {
			value = value === "" ? undefined : Number(value);
		}
		this.value = value;
		this.onChange(value);
	}

	onBlur() {
		this.onTouched();
	}
}
