import { Component, OnInit } from '@angular/core';
import { ICourse } from 'src/app/models/course';
import { AuthService } from 'src/app/services/auth.service';
import { CoursesService } from '../courses.service';
import { AppComponent } from 'src/app/app.component';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})

export class ShopComponent implements OnInit {
  courses : ICourse[] = []
  ownedCourses: ICourse[] = [];

  constructor(private appComponent: AppComponent, private coursesService: CoursesService, private authService: AuthService) { }

  ngOnInit(): void {
    this.coursesService.getCourses().subscribe(results => {
      this.courses = results;
      (window as any).shopCategories = [...new Set(results.map(r => r.category))];
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
  
  filteredCourses(){
    if(this.appComponent.appliedFilters.size == 0)
      return this.courses;
    return this.courses.filter(c => this.appComponent.appliedFilters.has(c.category));
  }
}
