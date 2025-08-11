import {
	componentWrapperDecorator,
	Meta,
	moduleMetadata,
	StoryObj,
} from '@storybook/angular';
import { SelectComponent } from './select.component';
import { LabelComponent } from '../label/label.component';

export default {
	title: 'Select',
	component: SelectComponent,
	decorators: [
		moduleMetadata({
			imports: [LabelComponent],
		}),
		componentWrapperDecorator(
			(story) => `
            <div style="display: flex; flex-direction: column; gap: 10px; width: 300px;">
                <zs-label formControlId="select-id" label="Selecciona una opción" />
                <zs-select [id]="id" [options]="options" [placeholder]="placeholder" />
            </div>
        `
		),
	],
} as Meta<SelectComponent>;

type Story = StoryObj<SelectComponent>;

export const Default: Story = {
	args: {
		id: 'select-id',
		options: [
			{ label: 'Opción 1', value: 'option1' },
			{ label: 'Opción 2', value: 'option2' },
			{ label: 'Opción 3', value: 'option3' },
		],
		placeholder: 'Selecciona una opción',
	},
};
