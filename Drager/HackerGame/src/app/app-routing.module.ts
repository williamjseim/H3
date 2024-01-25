import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CheckIfLogin, CheckIfLogout, authGuardGuard } from 'src/Services/auth-guard.guard';
import { FrontPageComponent } from './front-page/front-page.component';
import { BasePageComponent } from './base-page/base-page.component';

const routes: Routes = [
  {path: '', component:FrontPageComponent, pathMatch:'full'},
  {path: 'Index', component:BasePageComponent,canActivate:[authGuardGuard], children:[
    {path:'', redirectTo:'Home', pathMatch:'full'},
  ]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
