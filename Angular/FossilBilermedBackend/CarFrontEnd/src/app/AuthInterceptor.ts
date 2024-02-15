import { Injectable } from "@angular/core";
import { HttpHandlerFn, HttpInterceptorFn, HttpRequest } from "@angular/common/http";
import { Observable } from "rxjs";

export const AuthInterceptor: HttpInterceptorFn = (req: HttpRequest<unknown>, next: HttpHandlerFn) => {
        try{

            const Token = localStorage.getItem('Token');
            
            if(!Token){
                return next(req);
            }
            const req1 = req.clone({
                headers: req.headers.set("Authorization", `Bearer `+Token),
            })
            return next(req1);
        }
        catch{
            console.log()
            return next(req)
        }
    }