import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { CourseComponent } from 'src/app/courses/course/course.component';
import { CoursesService } from 'src/app/courses/courses.service';
import { IRating } from 'src/app/models/ratings/rating';
import { RatingService } from 'src/app/services/rating.service';

@Component({
  selector: 'app-create-edit-rating-view',
  templateUrl: './create-edit-rating-view.component.html',
  styleUrls: ['./create-edit-rating-view.component.css']
})
export class CreateEditRatingViewComponent implements OnInit {
  rating: IRating = {id: "0", score: 0, review: "", userId: ""};
  currentCourseId: string;

  constructor(private ratingService: RatingService,
    private coursesService: CoursesService,
    private router: Router,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.currentCourseId = params['courseId'];
      this.ratingService.getUserRating(this.currentCourseId).subscribe(respose => {
        this.rating.score = respose.score;
        this.rating.review = respose.review;
      });
    });
  }

  getRating(courseId: string) {
    this.ratingService.getUserRating(this.currentCourseId)
  }

  submit() {
      this.ratingService.addRating(this.rating, this.currentCourseId).subscribe(response => {
        this.router.navigateByUrl('/course');
    });
  }
}
