import { Injectable } from '@angular/core';
import { UserProgress } from 'src/app/Models/UserProgressModel';

@Injectable({
  providedIn: 'root'
})
export class UserDataService {

  constructor() { }

  userprogress?:UserProgress;
  
}
