import { Component, OnInit } from '@angular/core';
import { ICourse } from 'src/app/models/course';
import { AuthService } from 'src/app/services/auth.service';
import { CoursesService } from '../courses.service';

@Component({
  selector: 'app-mycourses',
  templateUrl: './mycourses.component.html',
  styleUrls: ['./mycourses.component.css']
})
export class MycoursesComponent implements OnInit {
  courses: ICourse[];

  constructor(private coursesService: CoursesService, private authService: AuthService) { }

  ngOnInit(): void {
    this.getUserCourses();
  }

  getUserCourses(){
    this.coursesService.getUserCourses().subscribe(response => {
      this.courses = response;
    }, error => {
      console.log(error);
    });
  }
}
