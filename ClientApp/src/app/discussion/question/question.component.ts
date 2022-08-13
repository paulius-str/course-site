import { Component, Input, OnInit } from '@angular/core';
import { IQuestion } from 'src/app/models/discussion/question';
import { DiscussionService } from '../discussion.service';

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.css']
})
export class QuestionComponent implements OnInit {
  @Input() question: IQuestion;

  constructor(private discussionService: DiscussionService) { }

  ngOnInit(): void {
  }

  openQuestion(){
    this.discussionService.setCurrentQuestion(this.question);
  }
}
