import { Routes } from '@angular/router';
import { PageComponent } from './page/page.component';
import { AppComponent } from './app.component';

export const routes: Routes = [
    {path: "", component:AppComponent},
    {path:"page", component:PageComponent}
];
