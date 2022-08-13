import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { CoursesService } from 'src/app/courses/courses.service';
import { ICourseSection } from 'src/app/models/courseSection';

@Component({
  selector: 'app-new-section-view',
  templateUrl: './new-section-view.component.html',
  styleUrls: ['./new-section-view.component.css']
})
export class NewSectionViewComponent implements OnInit {
  section: ICourseSection = {name: "", description: ""};


  constructor(public activeModal: NgbActiveModal, private courseService: CoursesService) { }

  ngOnInit(): void {
    this.section.courseId = this.courseService.selectedCourse.id;
  }

  createSection(){
    this.courseService.createSection(this.section).subscribe(response => {

    });

    this.activeModal.close();
  }
}
