import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { CoursesService } from 'src/app/courses/courses.service';
import { ICourseElement } from 'src/app/models/courseElement';

@Component({
  selector: 'app-new-element-view',
  templateUrl: './new-element-view.component.html',
  styleUrls: ['./new-element-view.component.css']
})
export class NewElementViewComponent implements OnInit {
  element: ICourseElement = {name: ""};
  sectionId: string;

  constructor(public activeModal: NgbActiveModal, private courseService: CoursesService) { }
  

  ngOnInit(): void {
    if(this.sectionId)
      this.element.sectionId = this.sectionId;
  }

  createElement(){
    this.courseService.createElement(this.element).subscribe(response => {

    });

    this.activeModal.close();
  }
}
