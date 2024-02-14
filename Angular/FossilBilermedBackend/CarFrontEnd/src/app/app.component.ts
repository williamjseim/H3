import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CarTableComponent } from './car-table/car-table.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CarTableComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'CarFrontEnd';
}
