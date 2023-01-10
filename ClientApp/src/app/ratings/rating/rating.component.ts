import { Component, Input, OnInit } from '@angular/core';
import { IRating } from 'src/app/models/ratings/rating';

@Component({
  selector: 'app-rating',
  templateUrl: './rating.component.html',
  styleUrls: ['./rating.component.css']
})
export class RatingComponent implements OnInit {
  @Input() rating: IRating;

  constructor() { }

  ngOnInit(): void {
  }

}
