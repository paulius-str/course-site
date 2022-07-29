import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CoursesService } from 'src/app/courses/courses.service';
import { ICourseSection } from 'src/app/models/courseSection';

@Component({
  selector: 'app-new-section-view',
  templateUrl: './new-section-view.component.html',
  styleUrls: ['./new-section-view.component.css']
})
export class NewSectionViewComponent implements OnInit {
  section: ICourseSection = {name: "", description: ""};


  constructor(private router: Router, private courseService: CoursesService) { }

  ngOnInit(): void {
    this.section.courseId = this.courseService.selectedCourse.id;
  }

  createSection(){
    this.courseService.createSection(this.section).subscribe(response => {
      this.router.navigateByUrl('course-edit');
      return this.section;
    });
  }
}
