import { Component, input } from '@angular/core';
import { Geometry } from '../geometry';
import { Square } from '../square';
import { Rectangle } from '../rectangle';
import { CommonModule, NgIf} from '@angular/common';

@Component({
  selector: 'app-calculator',
  standalone: true,
  imports: [CommonModule, NgIf],
  templateUrl: './calculator.component.html',
  styleUrl: './calculator.component.scss'
})

export class CalculatorComponent {
  geometry:Geometry = new Geometry();


  sides:string[] = [];

  text1:string = "";
  text2:string = "";
  Square(){
    this.geometry = new Square();
  }
  
  Rectangle(){
    this.geometry = new Rectangle();
  }

  Area(){
    this.geometry.sides = this.sides;
    this.text1 = this.geometry.Area().toString();
    this.geometry = new Geometry();
    this.sides = []
  }
  
  Omkreds(){
    this.geometry.sides = this.sides;
    this.text2 = this.geometry.Omkreds().toString();
    this.geometry = new Geometry();
    this.sides = []
  }
}
