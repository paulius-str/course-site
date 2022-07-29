import { Component, OnInit } from '@angular/core';
import { ICourse } from 'src/app/models/course';
import { AuthService } from 'src/app/services/auth.service';
import { CoursesService } from '../courses.service';
import { AppComponent } from 'src/app/app.component';

@Component({
  selector: 'app-mycourses',
  templateUrl: './mycourses.component.html',
  styleUrls: ['./mycourses.component.css']
})
export class MycoursesComponent implements OnInit {
  courses: ICourse[];

  constructor(private appComponent: AppComponent, private coursesService: CoursesService, private authService: AuthService) { }

  ngOnInit(): void {
    this.getUserCourses();
  }

  getUserCourses(){
    this.coursesService.getUserCourses().subscribe(response => {
      (window as any).ownedCoursesCategories = [...new Set(response.map(r => r.category))];
      this.courses = response;
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
