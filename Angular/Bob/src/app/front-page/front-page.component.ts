import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-front-page',
  standalone: true,
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './front-page.component.html',
  styleUrl: './front-page.component.scss'
})
export class FrontPageComponent {
  constructor(){

  }

}
