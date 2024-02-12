import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Geometry  } from './geometry';
import { Square } from './square';
import { Rectangle } from './rectangle';
import { CalculatorComponent } from './calculator/calculator.component';

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss',
    imports: [RouterOutlet, CalculatorComponent]
})
export class AppComponent {
  title = 'Geometri';
  geometry:Geometry = new Geometry();

  Square(){
    this.geometry = new Square();
  }

  Rectangle(){
    this.geometry = new Rectangle();
  }
}
