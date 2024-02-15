import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { CarTableComponent } from './car-table/car-table.component';
import { Router } from '@angular/router';
import { CarHttpService } from './car-http.service';
import {MatTableModule} from '@angular/material/table';
import { Observable } from 'rxjs';
import { CarModel } from './car-model';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CarTableComponent, RouterModule, MatTableModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'CarFrontEnd';

  constructor(private router:Router, private carhttp:CarHttpService){}

  Permission(){
    this.router.navigate(["Permission"])
  }
  
  NewPage(){
    this.router.navigate(["NewPage"])
  }

  GetToken(role:string){
    this.carhttp.GetToken(role).subscribe({
      next:(token)=>{
        localStorage.setItem("Token", token);
      }
    })
  }

  ValidateToken(){
    this.carhttp.Validate().subscribe({
      next:(token)=>{
        console.log(token);
      }
    })
  }
}
