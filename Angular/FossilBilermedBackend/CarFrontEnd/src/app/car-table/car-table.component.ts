import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Observable } from 'rxjs';
import { CarModel } from '../car-model';
import { CarHttpService } from '../car-http.service';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-car-table',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './car-table.component.html',
  styleUrl: './car-table.component.scss'
})
export class CarTableComponent {

  constructor(private carHttp:CarHttpService) {}
  cardataObserver$?:Observable<CarModel[]>;
  CarList?:CarModel[];

  CarForm = new FormGroup({
    rank:new FormControl("",[Validators.required]),
    model:new FormControl("",[Validators.required]),
    numbersold:new FormControl("",[Validators.required]),
    percentagechange:new FormControl("",[Validators.required]),
  })

  Test(){
    console.log("asdwawd");
    this.cardataObserver$ = this.carHttp.GetAllCars();
    this.cardataObserver$.subscribe({next:(value)=>{this.CarList = value;}})
    
  }
  SubmitCar(){
    if(this.CarForm.valid)
    this.carHttp.PostCar(Number(this.CarForm.get("rank")?.value), this.CarForm.get("model")!.value as string, Number(this.CarForm.get("numbersold")!.value), Number(this.CarForm.get("percentagechange")!.value))
    .subscribe({next:(value)=>
    {
      if(value){
        console.log("success")
        this.CarList?.push(value);
      }
    }});
  }

  DeleteCar(car:CarModel, index:number){
    this.carHttp.DeleteCar(car.id!).subscribe({
      next:(message)=>{
        this.CarList!.splice(index, 1);
        console.log(message, index)
      }});
  }

  RandomColor():string{
    return "background-color:" + "#" + Math.floor(Math.random()*16777215).toString(16)+";";
  }

}
