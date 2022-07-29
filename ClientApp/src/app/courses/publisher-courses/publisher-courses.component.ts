import { Component, OnInit } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ICourse } from 'src/app/models/course';
import { CoursesService } from '../courses.service';
import { NewCourseCreationViewComponent } from './new-course-creation-view/new-course-creation-view.component';
import { AppComponent } from 'src/app/app.component';

@Component({
  selector: 'app-publisher-courses',
  templateUrl: './publisher-courses.component.html',
  styleUrls: ['./publisher-courses.component.css']
})
export class PublisherCoursesComponent implements OnInit {
  courses: ICourse[];

  
  constructor(private appComponent: AppComponent, private coursesService: CoursesService, public modalService: NgbModal) { }

  ngOnInit(): void {
    this.getCourses();
  }

  open() {
    const modalRef = this.modalService.open(NewCourseCreationViewComponent);
    modalRef.componentInstance.name = 'Create course';
    
    modalRef.closed.subscribe(response => {
      this.courses.push(response);
      setTimeout(() => {
        this.ngOnInit();
     }, 500);
      console.log("modal closed");
    });
    
  }

  getCourses(){
    this.coursesService.getPublisherCourses().subscribe(response => {
      this.courses = response;
      console.log(response);
      (window as any).publishedCoursesCategories = [...new Set(response.map(r => r.category))];
      console.log((window as any).publishedCoursesCategories);
    }, error => {
      console.log(error);
    });
  }

  filteredCourses(){
    if(this.appComponent.appliedFilters.size == 0)
      return this.courses;
    return this.courses.filter(c => this.appComponent.appliedFilters.has(c.category));
  }
}
