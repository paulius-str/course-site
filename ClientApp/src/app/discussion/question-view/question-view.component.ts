import { Component, OnInit } from '@angular/core';
import { IAnswer, IQuestion } from 'src/app/models/discussion/question';
import { DiscussionService } from '../discussion.service';

@Component({
  selector: 'app-question-view',
  templateUrl: './question-view.component.html',
  styleUrls: ['./question-view.component.css']
})
export class QuestionViewComponent implements OnInit {
  question: IQuestion;
  answers: IAnswer[];
  newAnswer: IAnswer = {text: ""};

  constructor(private discussionService: DiscussionService) {
    
  }

  ngOnInit(): void {
    this.question = this.discussionService.currentQuestion;

    if(this.question.id){
      this.discussionService.getAnswers(this.question.id).subscribe(response => {
        this.answers = response;
      });
    }
    
  }

  createAnswer(){
    console.log(this.question)
    if(this.question.id){
      this.discussionService.createAnswer(this.newAnswer, this.question.id).subscribe(response => {
        this.ngOnInit();
        this.newAnswer = {text: ""};
      });
    } 
  }
}
