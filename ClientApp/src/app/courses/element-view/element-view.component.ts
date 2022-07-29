import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DiscussionService } from 'src/app/discussion/discussion.service';
import { ICourseElement } from 'src/app/models/courseElement';
import { IQuestion } from 'src/app/models/discussion/question';
import { IElementContent } from 'src/app/models/element-content/elementContent';
import { CoursesService } from '../courses.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-element-view',
  templateUrl: './element-view.component.html',
  styleUrls: ['./element-view.component.css']
})
export class ElementViewComponent implements OnInit {
  element: ICourseElement;
  elementContent: IElementContent = {
    courseElement: {},
    articleContent: {id: "0", text: ""},
    videoContent: {id: "0", videoUrl: ""}
  };
  currentVideoId: string;
  videoUrl = "";
  courseElements: ICourseElement[];
  hasNextElement: boolean = false;
  nextElement: ICourseElement;
  questions: IQuestion[];
  newQuestion: IQuestion = {title: "", text: "", userId: ""};

  constructor(private coursesService: CoursesService, private router: Router, 
    private discussionService: DiscussionService) {
    
  }

  ngOnInit(): void {
    this.courseElements = this.coursesService.selectedCourseElements;
    this.element = this.coursesService.selectedElement;

    this.CheckNextElement();

    if(this.element.id){
      this.discussionService.getQuestions(this.element.id).subscribe(response => {
        this.questions = response;
      });
    }
     

    this.coursesService.getElementContent(this.element).subscribe(response => {
      this.elementContent = response;

      var videoUrl = this.elementContent.videoContent.videoUrl;
      this.videoUrl = "";

      if(videoUrl){
        this.videoUrl = videoUrl;
        this.selectVideo();
      }
        
    });
  }

  CheckNextElement(){
    var index = 0;

    for (let i = 0; i < this.courseElements.length; i++) {
      if(this.courseElements[i].id == this.element.id)
        index = i;
    }

    if(index >= this.courseElements.length - 1){
        this.hasNextElement = false;
        return;
    }
    
    index++;
    this.hasNextElement = true;
    this.nextElement = this.courseElements[index];  
  }

  loadNextElement(){
    this.markAsComplete();
    this.coursesService.selectElement(this.nextElement);
    this.ngOnInit();
  }

  markAsComplete(){
    this.coursesService.addCompletedElement(this.element).subscribe(response => {
      
    });
  }

  selectVideo() {
    const params = new URL(this.videoUrl).searchParams;
    var id = params.get('v');
    
    if(id != null)
      this.currentVideoId = id;
  }

  onQuestionSubmit(form: NgForm) {
    if (form.valid) {
      this.createQuestion();
    }
  }

  createQuestion(){
    if(this.element.id)
      this.discussionService.createQuestion(this.newQuestion, this.element.id).subscribe(response => {
        setTimeout(() => {
          console.log('sleep');
          this.ngOnInit();
        }, 1000);
        
      });
  }
}
