import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CarModel } from './car-model';
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';

@Injectable({
  providedIn: 'root'
})
export class CarHttpService {

  constructor(private http:HttpClient) { }

  baseAPiUrl = "https://localhost:7247";

  GetAllCars() : Observable<CarModel[]>{
    console.log("asdw");
    return this.http.get<CarModel[]>(this.baseAPiUrl+"/Car/GetCars");
  }

  PostCar(rank:number, model:string, numbersold:number, percent:number):Observable<CarModel>{
    return this.http.post<CarModel>(this.baseAPiUrl+`/Car/CreateCar/${rank}/${model}/${numbersold}/${percent}`,"")
  }

  DeleteCar(carId:string): Observable<string>{
    return this.http.delete<string>(this.baseAPiUrl+`/Car/DeleteCar/${carId}`);
  }

  UpdateCar(carId:string, car:CarModel): Observable<boolean>{
    return this.http.post<boolean>(this.baseAPiUrl+`/Car/UpdateCar/${carId}/${car.rank}/${car.model}/${car.numberSold}/${car.percentageChange}`,"")
  }

  GetToken(role:string): Observable<string>{
    return this.http.get<string>(this.baseAPiUrl+`/Car/GetToken/${role}`);
  }

  Validate() : Observable<boolean>{
    return this.http.get<boolean>(this.baseAPiUrl+"/Car/Verified");
  }
  
}
