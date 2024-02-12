import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive, Router } from '@angular/router';

@Component({
  selector: 'app-front-page',
  standalone: true,
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './front-page.component.html',
  styleUrl: './front-page.component.scss'
})
export class FrontPageComponent {
  constructor(private router: Router){}

  content = "";

  ngOnit(){
    this.content = "dasdwawd"
  }

  Page():void
  {
    console.log("asdwasd");
    this.router.navigate(['page']);
    this.content = "adwadwwads";
  }
}
