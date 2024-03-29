import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';
  appliedFilters: Set<string> = new Set<string>();

  constructor(public router: Router, public authService: AuthService) { 
  }
}
