import { Meta, StoryObj } from '@storybook/angular';
import { ButtonComponent } from './button.component';

export default {
	title: 'Button',
	component: ButtonComponent,
} as Meta<ButtonComponent>;

type Story = StoryObj<ButtonComponent>;

export const BasicButton: Story = {};
