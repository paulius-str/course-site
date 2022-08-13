import { Component, OnInit } from '@angular/core';
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
  rating: IRating = {id: "0", score: 0, review: ""};
  currentCourseId: string;

  constructor(public activeModal: NgbActiveModal, private ratingService: RatingService,
    private coursesService: CoursesService) { }

  ngOnInit(): void {
  }

  submit(){
    this.activeModal.close();
    this.ratingService.addRating(this.rating, this.coursesService.selectedCourse.id).subscribe(response => {

    });
  }
}
