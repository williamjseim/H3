import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map, tap } from 'rxjs';
import { environment } from 'src/Environments/Environment';
import { UserProgress, UserInventory, UserSoftware, BrowserData, TimedTask, CrackAnswer, BooleanAnswer } from 'src/app/Models/UserProgressModel';

@Injectable({
  providedIn: 'root'
})
export class UserPageService {

  constructor(private http:HttpClient) { }

  private BaseApiKey = environment.baseApiUrl;

  GetUserProgress(): Observable<UserProgress>{
    return this.http.get<UserProgress>(this.BaseApiKey+"/api/UserPage/Progress");
  }
  
  GetUserInventory(): Observable<UserInventory>{
    return this.http.get<UserInventory>(this.BaseApiKey+"/api/UserPage/Inventory");
  }
  

  SendUserOnMission(location:string):Observable<string>{
    return this.http.get<string>(this.BaseApiKey+"/api/UserPage/StartAdventure/"+location);
  }

  TryConnect(ip:string):Observable<BrowserData>{
    return this.http.get<BrowserData>(this.BaseApiKey+"/api/FakeBrowser/"+ip);
  }

  GetSoftware():Observable<UserSoftware[]>{
    return this.http.get<UserSoftware[]>(this.BaseApiKey+"/api/Userpage/Software");
  }

  TryCrack(ip:String):Observable<CrackAnswer>{
    return this.http.get<CrackAnswer>(this.BaseApiKey+"/api/FakeBrowser/Crack/"+ip);
  }

  GetTasks():Observable<TimedTask[]>{
    return this.http.get<TimedTask[]>(this.BaseApiKey+`/api/UserPage/Tasks`);
  }

  CompleteTask(TaskId:string):Observable<BooleanAnswer>{
    return this.http.put<BooleanAnswer>(this.BaseApiKey+`/api/UserPage/CompleteTask/${TaskId}`, "")
  }

  CancelTask(taskId:string):Observable<boolean>{
    return this.http.delete<boolean>(this.BaseApiKey+`/api/UserPage/CancelTask/${taskId}`)
  }

  DeleteSoftware(softwareId:string):Observable<boolean>{
    return this.http.put<boolean>(this.BaseApiKey+`/api/UserPage/DeleteSoftware/${softwareId}`, "")
  }

  InstallSoftware(softwareId:string):Observable<boolean>{
    return this.http.put<boolean>(this.BaseApiKey+`/api/UserPage/InstallSoftware/${softwareId}`,"")
  }

  UninstallSoftware(softwareId:string):Observable<boolean>{
    return this.http.put<boolean>(this.BaseApiKey+`/api/UserPage/UninstallSoftware/${softwareId}`,"")
  }

  GetActiveViruses():Observable<string | UserSoftware[]>{
    return this.http.get<string | UserSoftware[]>(this.BaseApiKey+`/api/UserPage/GetActiveViruses`);
  }

}