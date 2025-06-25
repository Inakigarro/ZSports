export interface Button {
	id: string;
	label?: string;
	icon?: string;
	iconPosition?: ButtonIconPosition;
	type?: ButtonType;
	disabled?: boolean;
}

export type ButtonType =
	| "primary"
	| "secondary"
	| "tertiary"
	| "danger"
	| "success";
export type ButtonIconPosition = "left" | "right";

export const defaultButton: Button = {
	id: "default-button",
	label: "Button",
	icon: "",
	iconPosition: "left",
	type: "primary",
	disabled: false,
};
