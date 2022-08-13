import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ICourse } from '../models/course';
import { IRating } from '../models/ratings/rating';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class RatingService {

  constructor(private http: HttpClient, private authService: AuthService) { }

  getRatings(courseId: string){
    return this.http.get<IRating[]>(environment.baseUrl + 'rating/' + courseId);
  }

  addRating(rating: IRating, courseId: string){
    return this.http.post(environment.baseUrl + 'rating/' + courseId + '/' + this.authService.appUser?.id, rating);
  }
}
