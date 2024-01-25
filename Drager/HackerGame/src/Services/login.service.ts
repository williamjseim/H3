import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map, tap } from 'rxjs';
import { environment } from 'src/Environments/Environment';
import { JwtToken } from 'src/app/Models/JwtToken';
import { User } from 'src/app/Models/User.model';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private baseApiUrl : string = environment.baseApiUrl;

  constructor(private http : HttpClient) { }

    CreateUser(body : User):Observable<boolean> { 
      body.Id = '00000000-0000-0000-0000-000000000000';
      return this.http.post<boolean>(this.baseApiUrl+'/api/UserLogin/CreateUser', body);
    };

    GetUsers():Observable<User[]>{
      return this.http.get<User[]>(this.baseApiUrl+'/api/UserLogin');
    }

    GetUser(username:string):Observable<boolean>{
      return this.http.get<boolean>(this.baseApiUrl+'/api/UserLogin/'+username);
    }

    Login(user:User):Observable<string>{
      return this.http.get<string>(this.baseApiUrl+'/api/UserLogin/'+user.Username+"/"+user.Password);
    }

    VerifyLogin():Observable<boolean>{
      return this.http.get<boolean>(this.baseApiUrl+'/VerifyLogin');
    }
}
