import { Component, Input, OnInit } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { ICourse } from 'src/app/models/course';
import { AuthService } from 'src/app/services/auth.service';
import { environment } from 'src/environments/environment';
import { CoursesService } from '../courses.service';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css']
})
export class CourseComponent implements OnInit {
  @Input() course: ICourse = {name: "", id: "", description: "", pictureUrl: "", price: ""};
  @Input() owned: boolean = false;
  @Input() editMode: boolean = false;
  baseUrl: string = environment.baseUrl;
  showBasket: boolean = true;

  constructor(private basketService: BasketService, public coursesService: CoursesService, public authService: AuthService) { }

  ngOnInit(): void {

  }

  addToBasket(){
    this.basketService.addToBasket(this.course);
    this.showBasket = false;
  }

  deleteCourse(){
    
  }
}
