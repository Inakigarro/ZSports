import { Component, Input } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-nav-item',
  standalone: true,
  templateUrl: './nav-item.component.html',
  styleUrl: './nav-item.component.scss',
  imports: [RouterLink, RouterLinkActive],
})
export class NavItemComponent {
  @Input() label: string = '';
  @Input() route: string | any[] = '';
  @Input() icon?: string;
}
