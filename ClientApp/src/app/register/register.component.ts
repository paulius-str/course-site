import { Component, OnInit } from '@angular/core';
import {NgbCalendar, NgbDateStruct} from '@ng-bootstrap/ng-bootstrap';
import { IUserRegisterDto } from '../models/userRegisterDto';
import { AuthService } from '../services/auth.service';
import { FormsModule, NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  imports: [FormsModule],
})
export class RegisterComponent implements OnInit {
  model: NgbDateStruct = {year: 1994, month: 1, day: 1};
  registerDto: IUserRegisterDto = {emailAddress: "", username: "", firstName: "", lastName: "", birthDate: new Date(), password: "", repeatedPassword: "" };
  date: Date = new Date();
  showError: boolean = false;

  constructor(private calendar: NgbCalendar, private authService: AuthService, private router: Router) { 

  }

  ngOnInit(): void {
   
  }

  register(){
    
    var validationErrors = this.validateData();

    var result = this.authService.register(this.registerDto);
    
    if(result == null)
    {
      this.showError = true;
    }
  }

  validateData() {
    const errors = [];
    var error = '';
    if(this.isNullOrEmpty(this.registerDto.emailAddress))
      errors.push("Email");
    if(this.isNullOrEmpty(this.registerDto.username))
      errors.push("Username");
    if(this.isNullOrEmpty(this.registerDto.firstName))
      errors.push("First Name");
    if(this.isNullOrEmpty(this.registerDto.lastName))
      errors.push("Last Name");
      if(this.isNullOrEmpty(this.registerDto.password))
      errors.push("Password");
      if(this.isNullOrEmpty(this.registerDto.repeatedPassword))
      errors.push("Repeated Password");
    if(this.registerDto.password != this.registerDto.password)
      errors.push("Passwords does not match");
    
    return errors;
  }

  passwordsMatch(){
    if (this.registerDto.password == this.registerDto.repeatedPassword)
      return true;

    return false;
  }

  selectToday() {
    this.model = this.calendar.getToday();
  }

  isNullOrEmpty(str: string | null | undefined): boolean {
    return !str || str.trim() === '';
  }

  onSubmit(form: NgForm) {
    if (form.valid && this.passwordsMatch()) {
      this.register();
    }
  }

  closeModal(event: MouseEvent) {
    if (!(event.target as HTMLElement).closest('.modal-custom')) {
      this.router.navigate(['/']);
    }
  }
  
}
