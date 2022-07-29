import { Component, Input, OnInit } from '@angular/core';
import { IQuestion } from 'src/app/models/discussion/question';
import { DiscussionService } from '../discussion.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.css']
})
export class QuestionComponent implements OnInit {
  @Input() question: IQuestion;
  @Input() open: boolean;
  username: string;

  constructor(private discussionService: DiscussionService, public authService: AuthService) { }

  ngOnInit(): void {
    this.getUsername(this.question.userId);
  }

  openQuestion(){
    console.log("open question");
    this.discussionService.setCurrentQuestion(this.question);
  }

  getUsername(userId: string){
    return this.discussionService.getUsername(userId).subscribe(response => {
      this.username = response;
    });
  }
}
