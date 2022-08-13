import { Component, OnInit } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ICourse } from 'src/app/models/course';
import { CoursesService } from '../courses.service';
import { NewCourseCreationViewComponent } from './new-course-creation-view/new-course-creation-view.component';

@Component({
  selector: 'app-publisher-courses',
  templateUrl: './publisher-courses.component.html',
  styleUrls: ['./publisher-courses.component.css']
})
export class PublisherCoursesComponent implements OnInit {
  courses: ICourse[];

  
  constructor(private coursesService: CoursesService, public modalService: NgbModal) { }

  ngOnInit(): void {
    this.getCourses();
  }

  open() {
    const modalRef = this.modalService.open(NewCourseCreationViewComponent);
    modalRef.componentInstance.name = 'Create course';
    
    modalRef.closed.subscribe(response => {
      this.getCourses();
    });
    
  }

  getCourses(){
    this.coursesService.getPublisherCourses().subscribe(response => {
      this.courses = response;
    }, error => {
      console.log(error);
    });
  }
}
