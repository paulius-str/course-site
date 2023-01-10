import { Component, OnInit } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ICourse } from 'src/app/models/course';
import { ICourseForCreationDto } from 'src/app/models/courseForCreation';
import { CoursesService } from '../../courses.service';

@Component({
  selector: 'app-new-course-creation-view',
  templateUrl: './new-course-creation-view.component.html',
  styleUrls: ['./new-course-creation-view.component.css']
})
export class NewCourseCreationViewComponent implements OnInit {
  course: ICourseForCreationDto = {userId: "", name: "", description: "", price: 0};

  constructor(public activeModal: NgbActiveModal, private coursesService: CoursesService) { }
  

  ngOnInit(): void {
    
  }

  createCourse(){
    this.coursesService.createCourse(this.course).subscribe(response => {
      return this.course;
    });

    this.activeModal.close();
  }
}
