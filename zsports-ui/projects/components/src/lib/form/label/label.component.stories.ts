import {
	componentWrapperDecorator,
	Meta,
	moduleMetadata,
	StoryObj,
} from '@storybook/angular';
import { LabelComponent } from './label.component';
import { InputComponent } from '../input/input.component';

export default {
	title: 'Label',
	component: LabelComponent,
	decorators: [
		moduleMetadata({
			imports: [InputComponent],
		}),
		componentWrapperDecorator(
			(story) => `
            <div style="display: flex; flex-direction: column; gap: 10px; width: 300px;">
                <zs-label [formControlId]="'label-id'" [label]="'Nombre'" />
                <zs-input [id]="'label-id'" [type]="text" [placeholder]="'Ingrese su nombre'" />
            </div>
            `
		),
	],
} as Meta<LabelComponent>;

type Story = StoryObj<LabelComponent>;

export const Default: Story = {
	args: {
		formControlId: 'label-id',
		label: 'Nombre',
	},
};
