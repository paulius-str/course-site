import { Component } from '@angular/core';
import { BasketService } from '../basket/basket.service';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
    username: string;

    constructor(public authService: AuthService, public basketService: BasketService, public router: Router) {
      
    }

    ngOnInit(){
      this.authService.getUserObservable().subscribe(response => {
        console.log(response);
        this.username = response.name;
        console.log(this.username);
      });
    }

    getUserName() {
      return this.authService.appUser?.name;
    }
}
