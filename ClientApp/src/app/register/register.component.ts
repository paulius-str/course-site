import { Component, OnInit } from '@angular/core';
import {NgbCalendar, NgbDateStruct} from '@ng-bootstrap/ng-bootstrap';
import { IUserRegisterDto } from '../models/userRegisterDto';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model: NgbDateStruct = {year: 1994, month: 1, day: 1};
  registerDto: IUserRegisterDto = {emailAddress: "", username: "", firstName: "", lastName: "", birthDate: new Date(), password: ""};
  date: Date = new Date();
  showError: boolean = false;

  constructor(private calendar: NgbCalendar, private authService: AuthService) { 

  }

  ngOnInit(): void {
   
  }

  register(){
    this.date.setFullYear(this.model.year);
    this.date.setMonth(this.model.month - 1);
    this.date.setDate(this.model.day);
    this.registerDto.birthDate.setFullYear(this.model.year, this.model.month, this.model.day)
    var result = this.authService.register(this.registerDto);
    
    if(result == null)
    {
      this.showError = true;
    }
  }

  selectToday() {
    this.model = this.calendar.getToday();
  }
}
