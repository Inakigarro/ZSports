import { Meta, StoryObj } from '@storybook/angular';
import { InputComponent } from './input.component';

export default {
	title: 'Input',
	component: InputComponent,
} as Meta<InputComponent>;

type Story = StoryObj<InputComponent>;

export const Default: Story = {
	args: {
		id: 'input-id',
		type: 'text',
		placeholder: 'Ingrese su nombre',
		required: false,
		disabled: false,
	},
};

export const Required: Story = {
	args: {
		id: 'input-required',
		type: 'text',
		placeholder: 'Campo requerido',
		required: true,
		disabled: false,
	},
};

export const Disabled: Story = {
	args: {
		id: 'input-disabled',
		type: 'text',
		placeholder: 'Campo deshabilitado',
		required: false,
		disabled: true,
	},
};
