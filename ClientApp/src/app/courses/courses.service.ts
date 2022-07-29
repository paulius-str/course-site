import { HttpClient, JsonpInterceptor } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ICourse } from '../models/course';
import { ICourseElement } from '../models/courseElement';
import { ICourseForCreationDto } from '../models/courseForCreation';
import { ICourseSection } from '../models/courseSection';
import { IElementContent } from '../models/element-content/elementContent';
import { IUser } from '../models/user';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class CoursesService {
  course: ICourse[] = [];
  selectedCourse: ICourse;
  selectedElement: ICourseElement;
  selectedCourseElements: ICourseElement[];
  editMode: boolean = false;
  ownedCourses: ICourse[] = [];
  

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string,
  private authService: AuthService) {
    var selectedCourse = localStorage.getItem('selectedCourse');
    var selectedElement = localStorage.getItem('selectedElement')
    var selectedElements = localStorage.getItem('selectedElements')

    if(selectedCourse)
      this.selectedCourse = JSON.parse(selectedCourse);

    if(selectedElement)
      this.selectedElement = JSON.parse(selectedElement);

    if(selectedElements)
      this.selectedCourseElements = JSON.parse(selectedElements);   
  }

  setOwnedCourses(){
    this.getUserCourses().subscribe(response => {
      this.ownedCourses = response;
    })
  }

  isOwned(course: ICourse){
    var result = false;

    this.ownedCourses.forEach(x => {
      if(x.id == course.id)
        result = true;
    });

    return result;
  }

  selectCourse(course: ICourse){
    this.selectedCourse = course;
    localStorage.setItem('selectedCourse', JSON.stringify(this.selectedCourse));
  }

  selectElement(element: ICourseElement){
    this.selectedElement = element;
    localStorage.setItem('selectedElement', JSON.stringify(this.selectedElement));
  }

  selectElements(elements: ICourseElement[]){
    this.selectedCourseElements = elements;
    localStorage.setItem('selectedElements', JSON.stringify(this.selectedCourseElements));
  }

  getCourses(){
    console.log(environment.baseUrl);
    return this.http.get<ICourse[]>(environment.baseUrl + 'courses');
  }

  getUserCourses(){
    return this.http.get<ICourse[]>(environment.baseUrl + 'courses/user/' + this.authService.appUser?.id);
  }

  addPurchasedCourse(course: ICourse){
    return this.http.post(environment.baseUrl + 'courses/purchased/' + this.authService.appUser?.id, course);
  }

  getCourseSections(course: ICourse){
    return this.http.get<ICourseSection[]>(environment.baseUrl + 'courses/' + course.id + '/sections');
  }

  getSectionElements(section: ICourseSection){
    return this.http.get<ICourseElement[]>(environment.baseUrl + 'courses/sections/elements/' + section.id);
  }

  getPublisherCourses(){
    return this.http.get<ICourse[]>(environment.baseUrl + 'courses/author/' + this.authService.appUser?.id);
  }

  getPublishedCourse(courseId: string){
    return this.http.get<ICourse>(environment.baseUrl + 'courses/published/' + courseId);
  }

  publishCourse(course: ICourse){
    return this.http.post<ICourse>(environment.baseUrl + 'courses/published', course);
  }

  unpublishCourse(course: ICourse){
    return this.http.delete<ICourse>(environment.baseUrl + 'courses/published/' + course.id);
  }

  createCourse(course: ICourseForCreationDto){
    
    if(this.authService.appUser)
      course.userId = this.authService.appUser?.id;

    return this.http.post<ICourse>(environment.baseUrl + 'courses/', course);
  }

  createCourseWithImage(course: ICourseForCreationDto, image: File) {
    const formData = new FormData();
    formData.append('course', JSON.stringify(course));
    formData.append('image', image);

    return this.http.post(environment.baseUrl + 'courses/', formData);
  }

  editCourse(course: ICourse){
    return this.http.put<ICourse>(environment.baseUrl + 'courses/', course);
  }
  
  deleteSection(sectionId: string){
    return this.http.delete(environment.baseUrl + 'courses/sections/' + sectionId)
  }

  createSection(section: ICourseSection){
    return this.http.post<ICourseSection>(environment.baseUrl + 'courses/sections', section);
  }

  editSection(section: ICourseSection){
    return this.http.put<ICourseSection>(environment.baseUrl + 'courses/sections', section);
  }

  createElement(element: ICourseElement){
    return this.http.post<ICourseElement>(environment.baseUrl + 'courses/elements', element);
  }

  editElement(element: ICourseElement){
    return this.http.put<ICourseElement>(environment.baseUrl + 'courses/elements', element);
  }

  deleteElement(elementId: string){
    return this.http.delete(environment.baseUrl + 'courses/elements/' + elementId)
  }

  getCourseAuthors(course: ICourse){
    return this.http.get<IUser[]>(environment.baseUrl + 'courses/' + course.id + '/authors');
  }

  getCompletedElements(course: ICourse){
    return this.http.get<ICourseElement[]>(environment.baseUrl + 'courses/elements/completed/' + this.authService.appUser?.id + '/' + course.id);
  }

  getElementContent(element: ICourseElement){
    return this.http.get<IElementContent>(environment.baseUrl + 'courses/elements/content/' + element.id)
  }

  setElementContent(elementContent: IElementContent, elementId?: string){
    return this.http.post(environment.baseUrl + 'courses/elements/content/' + elementId, elementContent);
  }

  deleteElementContent(){
    
  }

  addCompletedElement(element: ICourseElement){
    return this.http.post(environment.baseUrl + 'courses/elements/completed/' + this.authService.appUser?.id, element);
  }
}
