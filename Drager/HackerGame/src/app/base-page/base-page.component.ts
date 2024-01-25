import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-base-page',
  templateUrl: './base-page.component.html',
  styleUrls: ['./base-page.component.scss']
})
export class BasePageComponent {

  constructor(private router:Router, private route:ActivatedRoute){}

  HomePage(){
    this.router.navigate(['Index']);
  }

  MapPage(){
    this.router.navigate(['Map'],{relativeTo:this.route});
  }

  BrowserPage(){
    this.router.navigate(['Browser/1.2.3.4'],{relativeTo:this.route});
  }
  
  InventoryPage(){
    this.router.navigate(['Inventory'],{relativeTo:this.route});
  }

  EquipmentPage(){
    this.router.navigate(['Equipment'],{relativeTo:this.route});
  }

  TaskPage(){
    this.router.navigate(['Task'],{relativeTo:this.route})
  }

  Databases(){
    this.router.navigate(['Databases'],{relativeTo:this.route})
  }

  LogOut(){
    localStorage.clear();
    sessionStorage.clear();
    this.router.navigate(['']);
  }
  navbarCollapsed:boolean = true;
  navbarClass:string = 'NavBarCollapse';
  CollapseNavbar(){
    this.navbarCollapsed = !this.navbarCollapsed;
    this.navbarClass = (this.navbarCollapsed == false) ? 'NavBar' : 'NavBarCollapse';
  }
}
