import { Meta, StoryObj } from '@storybook/angular';
import { ButtonComponent } from './button.component';

export default {
	title: 'Button',
	component: ButtonComponent,
} as Meta<ButtonComponent>;

type Story = StoryObj<ButtonComponent>;

export const BasicButton: Story = {
	args: {
		id: 'basic-button',
		label: 'Click Me',
		type: 'primary',
		icon: '',
		iconPosition: 'left',
		disabled: false,
		hideLabelOnMobile: false,
	},
};

export const BasicButtonWithIcon: Story = {
	args: {
		id: 'button-with-icon',
		label: 'Guardar',
		type: 'primary',
		icon: 'fa-solid fa-save',
		iconPosition: 'left',
		disabled: false,
		hideLabelOnMobile: false,
	},
};

export const SecondaryButton: Story = {
	args: {
		id: 'secondary-button',
		label: 'Secondary Action',
		type: 'secondary',
		icon: '',
		iconPosition: 'left',
		disabled: false,
		hideLabelOnMobile: false,
	},
};

export const DangerButton: Story = {
	args: {
		id: 'danger-button',
		label: 'Delete',
		type: 'danger',
		icon: 'fa-solid fa-trash',
		iconPosition: 'left',
		disabled: false,
		hideLabelOnMobile: false,
	},
};

export const SuccessButton: Story = {
	args: {
		id: 'success-button',
		label: 'Success',
		type: 'success',
		icon: 'fa-solid fa-check',
		iconPosition: 'left',
		disabled: false,
		hideLabelOnMobile: false,
	},
};

export const DisabledButton: Story = {
	args: {
		id: 'disabled-button',
		label: 'Disabled',
		type: 'primary',
		icon: '',
		iconPosition: 'left',
		disabled: true,
		hideLabelOnMobile: false,
	},
};
