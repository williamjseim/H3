import { Injectable } from "@angular/core";
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable()
export class AuthInterceptor implements HttpInterceptor{
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        try{

            const Token = JSON.parse(localStorage.getItem('token') as string);
            
            if(!Token){
                return next.handle(req);
            }
            
            const req1 = req.clone({
                headers: req.headers.set("Authorization", `Bearer `+Token['value']),
            })
            return next.handle(req1);
        }
        catch{return next.handle(req)}
    }
}