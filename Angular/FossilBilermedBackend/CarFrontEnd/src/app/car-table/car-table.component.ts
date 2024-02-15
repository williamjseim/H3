import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Observable } from 'rxjs';
import { CarModel } from '../car-model';
import { CarHttpService } from '../car-http.service';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import {MatSnackBar} from '@angular/material/snack-bar';
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';

@Component({
  selector: 'app-car-table',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatButtonModule, MatInputModule, MatFormFieldModule],
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

  CarEditForm = new FormGroup({
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

  carindex?:number;
  carId:string = "";
  EditCar(id:string, index:number){
    this.carId = id;
    this.carindex = index;
    let car = this.CarList![index];
    this.CarEditForm.setValue({
      rank: car.rank?.toString()!,
      model: car.model,
      numbersold: car.numberSold?.toString()!,
      percentagechange: car.percentageChange?.toString()!,
    })
  }

  UpdateCar(){
    if(this.CarEditForm.valid){
      let car:CarModel = {} as CarModel;
      car.rank = Number(this.CarEditForm.get("rank")?.value);
      car.model = this.CarEditForm.get("model")!.value as string;
      car.numberSold = Number(this.CarEditForm.get("numbersold")!.value);
      car.percentageChange = Number(this.CarEditForm.get("percentagechange")!.value)
      console.log(car);
      this.carHttp.UpdateCar(this.carId, car).subscribe({
        next:(awdwaw)=>{
          car.id = this.carId;
          this.CarList![this.carindex!] = car;
          this.carId = "";
          this.carindex = -1;
          this.CarEditForm.reset();
          
        }
      })
    }
  }

}
