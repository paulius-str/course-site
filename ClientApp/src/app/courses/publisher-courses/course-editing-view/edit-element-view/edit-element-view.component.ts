import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { CoursesService } from 'src/app/courses/courses.service';
import { ICourseElement } from 'src/app/models/courseElement';
import { IElementContent } from 'src/app/models/element-content/elementContent';

@Component({
  selector: 'app-edit-element-view',
  templateUrl: './edit-element-view.component.html',
  styleUrls: ['./edit-element-view.component.css']
})
export class EditElementViewComponent implements OnInit {
  element: ICourseElement = {name: "", order: 0};
  videoUrl: string = "";
  articleText: string = "";
  elementContent: IElementContent = {articleContent: {id: "0", text: ""},
   videoContent: {id: "0", videoUrl: ""}};
  
  constructor(public activeModal: NgbActiveModal, private courseService: CoursesService) { }

  ngOnInit(): void {
    this.courseService.getElementContent(this.element).subscribe(response => {
      this.elementContent = response;
    })
  }

  addOrEditElement(){
    this.elementContent.articleContent.text = this.articleText;
    this.elementContent.videoContent.videoUrl = this.videoUrl;

    this.courseService.setElementContent(this.elementContent, this.element.id).subscribe(response =>{

    }, error => {
      console.log(error);
    });

    this.activeModal.close();
  }

  close(){
    this.activeModal.close();
  }
}
