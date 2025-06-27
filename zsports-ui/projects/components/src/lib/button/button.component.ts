import { CommonModule } from '@angular/common';
import { Component, input, OnDestroy, OnInit, output } from '@angular/core';

type ButtonType = 'primary' | 'secondary' | 'tertiary' | 'danger' | 'success';
type IconPosition = 'left' | 'right';

@Component({
	selector: 'zs-button',
	standalone: true,
	templateUrl: './button.component.html',
	styleUrl: './button.component.scss',
	imports: [CommonModule],
})
export class ButtonComponent implements OnInit, OnDestroy {
	// Inputs.
	public id = input.required<string>();
	public label = input<string>();
	public icon = input<string>();
	public type = input<ButtonType>('primary');
	public iconPosition = input<IconPosition>('left');
	public disabled = input<boolean>(false);
	public hideLabelOnMobile = input<boolean>(false);

	// Outputs.
	public onClick = output<string>();

	// Properties.
	protected isMobile: boolean = false;
	private resizeListener = () => {
		this.isMobile = window.matchMedia('(max-width: 600px)').matches;
	};

	ngOnInit() {
		this.resizeListener();
		window.addEventListener('resize', this.resizeListener);
	}

	ngOnDestroy() {
		window.removeEventListener('resize', this.resizeListener);
	}
}
