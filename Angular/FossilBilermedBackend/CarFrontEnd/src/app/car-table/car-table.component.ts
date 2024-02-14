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
        let car = {} as CarModel;
        car.rank = Number(this.CarForm.get("rank")!.value);
        car.model = this.CarForm.get("model")!.value as string;
        car.numberSold = Number(this.CarForm.get("rank")!.value);
        car.percentageChange = Number(this.CarForm.get("rank")!.value);
        this.CarList?.push(car);
      }
    }});
  }

  RandomColor():string{
    return "background-color:" + "#" + Math.floor(Math.random()*16777215).toString(16)+";";
  }

}
