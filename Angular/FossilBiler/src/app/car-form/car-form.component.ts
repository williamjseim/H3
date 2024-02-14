import { Component } from '@angular/core';
import { FormGroup, FormControl, ReactiveFormsModule } from '@angular/forms';
import { Icar } from '../icar';

@Component({
  selector: 'app-car-form',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './car-form.component.html',
  styleUrl: './car-form.component.scss'
})
export class CarFormComponent {
  carForm = new FormGroup({
    nummer: new FormControl({}),
    model: new FormControl({}),
    antal: new FormControl({}),
    procent: new FormControl({}),
  })

  Icar: Icar = {} as Icar;
  
  SubmitCar(){
    
  }
  
}
