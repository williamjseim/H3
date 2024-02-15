import { Injectable, inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, RouterStateSnapshot } from '@angular/router';
import { Router } from '@angular/router';
import { CarHttpService } from './car-http.service';
import { catchError, map, of } from 'rxjs';

export const authGuardGuard: CanActivateFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const router = inject(Router);
  const carhttp = inject(CarHttpService);
  let answer = false;
  carhttp.Validate().subscribe({
    next:(bool)=>{
      answer = bool;
      console.log(answer);
    }
  })
  return carhttp.Validate().pipe(
    map(()=> true),
    catchError(()=> {router.navigate(['']); return of(false)})
      );
};