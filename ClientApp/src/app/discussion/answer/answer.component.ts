import { Component, Input, OnInit } from '@angular/core';
import { IAnswer } from 'src/app/models/discussion/question';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-answer',
  templateUrl: './answer.component.html',
  styleUrls: ['./answer.component.css']
})
export class AnswerComponent implements OnInit {
  @Input() answer: IAnswer;
  isOwner: boolean = false;

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    if(this.authService.appUser?.id === this.answer.userId){
      this.isOwner = true;
    }
  }


}
