import { Injectable } from '@angular/core';
import { CoursesService } from '../courses/courses.service';
import { ICourse } from '../models/course';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  courses: ICourse[] = [];

  constructor(private coursesService: CoursesService) { }

  public addToBasket(course: ICourse){
    if(!this.courses.includes(course))
    this.courses.push(course);
  }

  getItems(){
    return this.courses;
  }

  checkout(){
    this.courses.forEach(course => {
      this.coursesService.addPurchasedCourse(course).subscribe(response => {

      });
    });

    this.courses = [];
  }
}
