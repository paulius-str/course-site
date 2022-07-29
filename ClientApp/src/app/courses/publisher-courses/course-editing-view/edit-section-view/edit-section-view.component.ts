import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CoursesService } from 'src/app/courses/courses.service';
import { ICourseSection } from 'src/app/models/courseSection';

@Component({
  selector: 'app-new-section-view',
  templateUrl: './edit-section-view.component.html',
  styleUrls: ['./edit-section-view.component.css']
})
export class EditSectionViewComponent implements OnInit {
  section: ICourseSection = {name: "", description: ""};


  constructor(private router: Router, private courseService: CoursesService) { }

  ngOnInit(): void {
    this.section.courseId = this.courseService.selectedCourse.id;
  }

  editSection(){
    this.courseService.editSection(this.section).subscribe(response => {
      this.router.navigateByUrl('course-edit');
      return this.section;
    });
  }
}
