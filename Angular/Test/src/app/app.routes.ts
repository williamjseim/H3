import { Routes } from '@angular/router';
import { FrontPageComponent } from './front-page/front-page.component';
import { PageComponent } from './page/page.component';

export const routes: Routes = [
    {path: "", component:FrontPageComponent},
    {path:"page", component:PageComponent}
];
