import { Component, Input, OnInit } from '@angular/core';
import { IAnswer } from 'src/app/models/discussion/question';
import { AuthService } from 'src/app/services/auth.service';
import { DiscussionService } from '../discussion.service';

@Component({
  selector: 'app-answer',
  templateUrl: './answer.component.html',
  styleUrls: ['./answer.component.css']
})
export class AnswerComponent implements OnInit {
  @Input() answer: IAnswer;
  isOwner: boolean = false;
  username: string;

  constructor(public authService: AuthService, public discussionService: DiscussionService) { }

  ngOnInit(): void {
    if(this.answer.userId != null)
      this.getUsername(this.answer.userId);

    if(this.authService.appUser?.id === this.answer.userId){
      this.isOwner = true;
    }
  }

  getUsername(userId: string){
    return this.discussionService.getUsername(userId).subscribe(response => {
      this.username = response;
    });
  }
}
