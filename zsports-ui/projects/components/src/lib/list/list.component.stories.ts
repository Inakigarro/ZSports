import { Meta, StoryObj } from '@storybook/angular';
import { ListComponent } from './list.component';

interface Item {
	id: string;
	number: number;
	description: string;
}

export default {
	title: 'List',
	component: ListComponent,
} as Meta<ListComponent<Item>>;

type Story = StoryObj<ListComponent<Item>>;

export const BasicList: Story = {
	args: {
		id: 'basic-list',
		columns: [
			{ key: 'number', label: 'Number', align: 'left', width: '100px' },
			{ key: 'description', label: 'Description', align: 'left' },
		],
		items: [
			{ id: '1', number: 1, description: 'Item 1' },
			{ id: '2', number: 2, description: 'Item 2' },
			{ id: '3', number: 3, description: 'Item 3' },
		],
	},
};
