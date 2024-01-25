import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot } from '@angular/router';
import { LoginService } from './login.service';
import { catchError, map, of } from 'rxjs';

export const authGuardGuard: CanActivateFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const loginservice = inject(LoginService);
  const router = inject(Router);
  return loginservice.VerifyLogin().pipe(
    map(()=> true),
    catchError(()=> {router.navigate(['FallBackPage']); return of(false)})
      );
};

export const CheckIfLogin: CanActivateFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const router = inject(Router);
  let token = localStorage.getItem('Token')
  if(token == null){
    router.navigate(['']);
    return false;
  }
  return true;
};

export const CheckIfLogout: CanActivateFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const router = inject(Router);
  let token = localStorage.getItem('Token');
  if(token != null){
    return true;
  }
  router.navigate(['UserPage']); 
  return false;
};