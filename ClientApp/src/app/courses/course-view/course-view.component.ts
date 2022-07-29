import { ApplicationRef, ChangeDetectorRef, Component, NgZone, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ICourse } from 'src/app/models/course';
import { ICourseElement } from 'src/app/models/courseElement';
import { ICourseSection } from 'src/app/models/courseSection';
import { ICourseSectionWithElements } from 'src/app/models/courseSectionWIthElements';
import { IElementContent } from 'src/app/models/element-content/elementContent';
import { IRating } from 'src/app/models/ratings/rating';
import { IUser } from 'src/app/models/user';
import { CreateEditRatingViewComponent } from 'src/app/ratings/create-edit-rating-view/create-edit-rating-view.component';
import { RatingComponent } from 'src/app/ratings/rating/rating.component';
import { RatingService } from 'src/app/services/rating.service';
import { CoursesService } from '../courses.service';

@Component({
  selector: 'app-course-view',
  templateUrl: './course-view.component.html',
  styleUrls: ['./course-view.component.css']
})
export class CourseViewComponent implements OnInit {
  isCollapsed = true;
  course: ICourse;
  sections: ICourseSection[];
  sectionsWithElements: ICourseSectionWithElements[] = [];
  authors: IUser[];
  allElements: ICourseElement[] = [];
  completeElements: ICourseElement[] = [];
  ratings: IRating[] = [];
  isOwnedBool: boolean = false;
  ownedCourses: ICourse[];

  constructor(public coursesService: CoursesService, private ratingService: RatingService,
      private modalService: NgbModal) { }

  ngOnInit(): void {
    this.course = this.coursesService.selectedCourse;

    this.sections = [];
    this.sectionsWithElements = [];
    this.allElements = [];
    this.completeElements = [];

    this.getSectionsWithElements();

    this.coursesService.getCourseAuthors(this.course).subscribe(response => {
      this.authors = response;
    });

    this.ratingService.getRatings(this.course.id).subscribe(response => {
      this.ratings = response;
    });

    this.isOwnedBool = this.coursesService.isOwned(this.course);

    this.coursesService.getUserCourses().subscribe(response => {
     this.ownedCourses = response;
     this.isOwnedBool = this.isOwned(this.course);
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

  getTotalLength(elements: ICourseElement[]){
    const lengths = elements.map(e => e.length);
    let sum = 0;
    lengths.forEach(num => {
      if(num != null) {
        sum += num;
      }  
    });
    return sum;
  }

  sumArray(numbers: number[]): number {
    let sum = 0;
    numbers.forEach(num => {
      sum += num;
    });
    return sum;
  }

  getCompletedElements(){
    this.coursesService.getCompletedElements(this.course).subscribe(response => {
      this.completeElements = response;
      //this.findAllElements();
    });
  }

  getSectionsWithElements(){
    this.coursesService.getCourseSections(this.course).subscribe(response => {
      this.sections = response;

      this.sections.forEach(section => {
        var sectionWithElements: ICourseSectionWithElements = {section: section, elements: [], closed: true};
        
        this.coursesService.getSectionElements(section).subscribe(elements => {
          sectionWithElements.elements = elements;
          
          elements.forEach(element => {this.allElements.push(element);});

        },error => {
          console.log(error);
        });

        this.sectionsWithElements.push(sectionWithElements);
      });

      this.getCompletedElements();

    });
  }

  openRatingView() {
    const modalRef = this.modalService.open(CreateEditRatingViewComponent);
    modalRef.componentInstance.name = 'Create Rating';
    modalRef.componentInstance.courseId = this.course.id;
    modalRef.closed.subscribe(response => {
      setTimeout(() => {
       this.ngOnInit();
     }, 500);
    });   
  }

  findAllElements(){
    this.sectionsWithElements.forEach(sectionWithElements => {
      sectionWithElements.elements.forEach(element => {
        this.allElements.push(element);
      });
    });
  }

  selectElement(element: ICourseElement){
    this.coursesService.selectElement(element);
    this.coursesService.selectElements(this.allElements);
  }

  isComplete(element: ICourseElement){
    var result = false;

    this.completeElements.forEach(x => {
      if(x.id == element.id){
        result = true;
      }   
    });

    return result;
  }
}
