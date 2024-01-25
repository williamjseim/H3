import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/Environments/Environment';
import { BooleanAnswer, SecretKey, UserSoftware } from 'src/app/Models/UserProgressModel';

@Injectable({
  providedIn: 'root'
})
export class BrowserService {

  constructor(private http:HttpClient) { }
  private BaseApiKey = environment.baseApiUrl;

  TryLoginToIp(ip:string, username:string, password:string): Observable<SecretKey | BooleanAnswer>{
    return this.http.get<SecretKey | BooleanAnswer>(this.BaseApiKey+`/Api/FakeBrowser/Login/${ip}/${username}/${password}`);
  }

  GetSoftware(ip:string, secretKey:string) : Observable<UserSoftware[]>{
    return this.http.get<UserSoftware[]>(this.BaseApiKey+`/Api/FakeBrowser/GetProgress/${ip}/${secretKey}`);
  }

  DownloadSoftware(ip:string, secretKey:string, softwareId:string): Observable<boolean>{
    return this.http.put<boolean>(this.BaseApiKey+`/Api/FakeBrowser/Download/${ip}/${secretKey}/${softwareId}`, "")
  }

  GetUploadableSoftware():Observable<UserSoftware[]>{
    return this.http.get<UserSoftware[]>(this.BaseApiKey+`/Api/FakeBrowser/GetUploadableSoftware`)
  }

  UploadSoftware(targetIp:string, softwareId:string):Observable<string>{
    return this.http.put<string>(this.BaseApiKey+`/Api/FakeBrowser/UploadSoftware/${targetIp}/${softwareId}`,"");
  }

  RemoteInstallSoftware(targetIp:string, softwareId:string):Observable<string>{
    return this.http.put<string>(this.BaseApiKey+`/Api/FakeBrowser/RemoteInstall/${targetIp}/${softwareId}`,"");
  }
}
