import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { CoursesService } from 'src/app/courses/courses.service';
import { ICourseElement } from 'src/app/models/courseElement';

@Component({
  selector: 'app-new-element-view',
  templateUrl: './new-element-view.component.html',
  styleUrls: ['./new-element-view.component.css']
})
export class NewElementViewComponent implements OnInit {
  element: ICourseElement = {name: "", length: 5};
  sectionId: string;

  constructor(private router: Router, private courseService: CoursesService, private route: ActivatedRoute) { }
  

  ngOnInit(): void {
  }

  createElement(){
    this.route.params.subscribe(params => {
      this.element.sectionId = params['sectionId'];
    });
    this.courseService.createElement(this.element).subscribe(response => {
      this.router.navigateByUrl('course-edit');
    });
  }
}
