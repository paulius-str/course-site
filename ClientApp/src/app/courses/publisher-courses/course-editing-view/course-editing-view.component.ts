import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ICourse } from 'src/app/models/course';
import { ICourseElement } from 'src/app/models/courseElement';
import { ICourseSection } from 'src/app/models/courseSection';
import { ICourseSectionWithElements } from 'src/app/models/courseSectionWIthElements';
import { IUser } from 'src/app/models/user';
import { CoursesService } from '../../courses.service';
import { EditElementViewComponent } from './edit-element-view/edit-element-view.component';
import { NewElementViewComponent } from './new-element-view/new-element-view.component';
import { NewSectionViewComponent } from './new-section-view/new-section-view.component';

@Component({
  selector: 'app-course-editing-view',
  templateUrl: './course-editing-view.component.html',
  styleUrls: ['./course-editing-view.component.css']
})
export class CourseEditingViewComponent implements OnInit {
  isCollapsed = true;
  course: ICourse;
  sections: ICourseSection[];
  sectionsWithElements: ICourseSectionWithElements[] = [];
  authors: IUser[];
  isPublished: boolean = false;
  

  constructor(public coursesService: CoursesService,  public modalService: NgbModal) { }

  ngOnInit(): void {
    this.sections = [];
    this.sectionsWithElements = [];
    this.course = this.coursesService.selectedCourse;

    this.coursesService.getCourseSections(this.course).subscribe(response => {
      this.sections = response;

      this.sections.forEach(section => {
        var sectionWithElements: ICourseSectionWithElements = {section: section, elements: [], closed: true};
        
        this.coursesService.getSectionElements(section).subscribe(elements => {
          sectionWithElements.elements = elements;
        },error => {
          console.log(error);
        });

        this.sectionsWithElements.push(sectionWithElements);
      });
    });

    this.coursesService.getCourseAuthors(this.course).subscribe(response => {
      this.authors = response;
    });

    this.checkIfPublished();
  }

  openNewSectionView() {
    const modalRef = this.modalService.open(NewSectionViewComponent);
    modalRef.componentInstance.name = 'Create Section';
    
    modalRef.closed.subscribe(response => {
     this.ngOnInit();
    });   
  }

  openNewElementView(sectionId?: string) {
    const modalRef = this.modalService.open(NewElementViewComponent);
    modalRef.componentInstance.name = 'Create Element';
    modalRef.componentInstance.sectionId = sectionId;

    modalRef.closed.subscribe(response => {
      this.ngOnInit();
    });  
  }

  openEditElementView(element: ICourseElement){
    const modalRef = this.modalService.open(EditElementViewComponent);
    modalRef.componentInstance.name = 'Create Element';
    modalRef.componentInstance.element = element;

    modalRef.closed.subscribe(response => {
      this.ngOnInit();
    });  
  }

  publishCourse(){
    this.coursesService.publishCourse(this.course).subscribe(response => {

    }, error => {
      console.log(error);
    });

    this.isPublished = true;
  }

  unpublishCourse(){
    this.coursesService.unpublishCourse(this.course).subscribe(response => {

    }, error => {
      console.log(error);
    });

    this.isPublished = false;
  }

  saveCourse(){
    this.coursesService.editCourse(this.course).subscribe(response => {
      this.coursesService.selectCourse(response);
    }, error => {
      console.log(error);
    })
  }

  checkIfPublished(){
    this.coursesService.getPublishedCourse(this.course.id).subscribe(response => {

      if(response.id != "0")
        this.isPublished = true;
      else
        this.isPublished = false;

    }, error => {
      this.isPublished = false;
      console.log(error);
    });
  }

  deleteSection(sectionId?: string){
    if(sectionId)
      this.coursesService.deleteSection(sectionId).subscribe(response => {});
  }

  deleteElement(elementId?: string){
    if(elementId)
      this.coursesService.deleteElement(elementId).subscribe(response => {});
  }
}
