import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { ICourse } from 'src/app/models/course';
import { AuthService } from 'src/app/services/auth.service';
import { environment } from 'src/environments/environment';
import { CoursesService } from '../courses.service';
import { RatingService } from 'src/app/services/rating.service';
import { IAverageRating } from 'src/app/models/ratings/averageRating';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css']
})
export class CourseComponent implements OnInit, OnDestroy {
  @Input() course: ICourse = {name: "", id: "", description: "", shortDescription: "", pictureUrl: "", price: "", category: ""};
  @Input() owned: boolean = false;
  @Input() editMode: boolean = false;
  baseUrl: string = environment.baseUrl;
  showBasket: boolean = true;
  averageRating: IAverageRating;
  ratingObservable: Observable<IAverageRating>;

  constructor(private basketService: BasketService, public coursesService: CoursesService,
     public authService: AuthService, public ratingService: RatingService) { }

  ngOnInit(): void {
    this.ratingService.getAverageRating(this.course.id).subscribe(response => {
      this.averageRating = response;
    })
  }

  ngOnDestroy(): void {
    
  }

  addToBasket(){
    this.basketService.addToBasket(this.course);
    this.showBasket = false;
  }

  deleteCourse(){
    
  }

  getImageUrl(){
    return this.baseUrl + this.course.pictureUrl;
  }
}
