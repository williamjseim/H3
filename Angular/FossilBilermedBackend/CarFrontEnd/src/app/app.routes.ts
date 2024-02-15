import { Routes } from '@angular/router';
import { NewPageComponent } from './new-page/new-page.component';
import { PermissionPageComponent } from './permission-page/permission-page.component';
import { CarTableComponent } from './car-table/car-table.component';
import { authGuardGuard } from './guard.service';

export const routes: Routes = [
    {path: "", component:CarTableComponent},
    {path: "NewPage", component:NewPageComponent},
    {path: "Permission", component:PermissionPageComponent, canActivate:[authGuardGuard]},
];
