import { Component, Input, OnInit } from '@angular/core';
import { IRating } from 'src/app/models/ratings/rating';
import { AuthService } from 'src/app/services/auth.service';
import { RatingService } from 'src/app/services/rating.service';

@Component({
  selector: 'app-rating',
  templateUrl: './rating.component.html',
  styleUrls: ['./rating.component.css']
})
export class RatingComponent implements OnInit {
  @Input() rating: IRating;
  username: string;

  constructor(public ratingService: RatingService, public authService: AuthService) { }

  ngOnInit(): void {
    this.ratingService.getUsername(this.rating.userId).subscribe(response => {
      this.username = response;
    });
  }

}
