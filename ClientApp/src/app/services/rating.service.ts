import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ICourse } from '../models/course';
import { IRating } from '../models/ratings/rating';
import { AuthService } from './auth.service';
import { IAverageRating } from '../models/ratings/averageRating';

@Injectable({
  providedIn: 'root'
})
export class RatingService {

  constructor(private http: HttpClient, private authService: AuthService) { }

  getRatings(courseId: string){
    return this.http.get<IRating[]>(environment.baseUrl + 'rating/' + courseId);
  }

  getUserRating(courseId: string){
    return this.http.get<IRating>(environment.baseUrl + 'rating/' + courseId + '/' + this.authService.appUser?.id);
  }

  addRating(rating: IRating, courseId: string){
    return this.http.post(environment.baseUrl + 'rating/' + courseId + '/' + this.authService.appUser?.id, rating);
  }

  getUsername(userId: string){
    return this.http.get(environment.baseUrl + 'users/nicknames/' + userId, { responseType: 'text' });
  }

  getAverageRating(courseId: string){
    return this.http.get<IAverageRating>(environment.baseUrl + 'rating/average/' + courseId);
  }
}
