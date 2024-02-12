import { Routes } from '@angular/router';
import { PageComponent } from './page/page.component';
import { FrontPageComponent } from './front-page/front-page.component';

export const routes: Routes = [
    {path: "", component:FrontPageComponent},
    {path:"page", component:PageComponent}
];
