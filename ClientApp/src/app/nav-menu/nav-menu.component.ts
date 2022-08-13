import { Component } from '@angular/core';
import { BasketService } from '../basket/basket.service';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {   
    constructor(public authService: AuthService, public basketService: BasketService) {
      
    }
}
