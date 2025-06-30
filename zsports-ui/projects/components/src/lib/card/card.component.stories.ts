import { componentWrapperDecorator, Meta, StoryObj } from '@storybook/angular';
import { CardComponent } from './card.component';

export default {
	title: 'Card',
	component: CardComponent,
	decorators: [
		componentWrapperDecorator(
			(story) => `
           <zs-card [id]="'card'">
                <div card-header>
                    <h2>Card Title</h2>
                </div>
                <div card-body>
                    <p>This is the body of the card.</p>
                </div>
                <div card-footer>
                    <button class="btn btn-primary">Action</button>
                </div>
           </zs-card>
        `
		),
	],
} as Meta<CardComponent>;

type Story = StoryObj<CardComponent>;

export const Default: Story = {
	args: {
		id: 'card',
	},
};
