import { Component, OnInit } from '@angular/core';
import { ICourse } from 'src/app/models/course';
import { AuthService } from 'src/app/services/auth.service';
import { CoursesService } from '../courses.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})

export class ShopComponent implements OnInit {
  courses : ICourse[] = []
  ownedCourses: ICourse[] = [];

  constructor(private coursesService: CoursesService, private authService: AuthService) { }

  ngOnInit(): void {
    this.coursesService.getCourses().subscribe(results => {
      this.courses = results;

      if(this.authService.appUser){
        this.coursesService.getUserCourses().subscribe(response => {
          this.ownedCourses = response;
        });
      }
        
      this.coursesService.setOwnedCourses();
    });
  }

  isOwned(course: ICourse){
    var result = false;

    this.ownedCourses.forEach(x => {
      if(x.id == course.id)
        result = true;
    });

    return result;
  }
}
