import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IAnswer, IQuestion } from '../models/discussion/question';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class DiscussionService {
  currentQuestion: IQuestion;

  constructor(private http: HttpClient, private authService: AuthService) { 
    var selectedQuestion = localStorage.getItem('currentQuestion');

    if(selectedQuestion)
      this.currentQuestion = JSON.parse(selectedQuestion);
  }

  setCurrentQuestion(question: IQuestion){
    this.currentQuestion = question;
    localStorage.setItem('currentQuestion', JSON.stringify(this.currentQuestion));
  }

  getQuestions(elementId: string){
    return this.http.get<IQuestion[]>(environment.baseUrl + 'discussion/' + elementId);
  }

  getAnswers(questionId: string){
    return this.http.get<IAnswer[]>(environment.baseUrl + 'discussion/answers/' + questionId);
  }

  createQuestion(question: IQuestion, elementId: string){
    return this.http.post(environment.baseUrl + 'discussion/' + elementId + '/' + this.authService.appUser?.id, question);
  }

  createAnswer(answer: IAnswer, questionId: String){
    return this.http.post(environment.baseUrl + 'discussion/answers/' + questionId + '/' + this.authService.appUser?.id, answer);
  }

  getUsername(userId: string){
    return this.http.get(environment.baseUrl + 'users/nicknames/' + userId, { responseType: 'text' });
  }
}
