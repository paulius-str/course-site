import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  email: string = "";
  password: string = "";
  showError: boolean = false;

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
  }

  login(){
    var result = this.authService.login({emailAddress: this.email, password: this.password});
    if(result == null)
    this.showError = true;
  }
}
