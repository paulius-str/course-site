import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Editor } from 'ngx-editor';
import { CoursesService } from 'src/app/courses/courses.service';
import { ICourseElement } from 'src/app/models/courseElement';
import { IElementContent } from 'src/app/models/element-content/elementContent';

@Component({
  selector: 'app-edit-element-view',
  templateUrl: './edit-element-view.component.html',
  styleUrls: ['./edit-element-view.component.css']
})
export class EditElementViewComponent implements OnInit {
  editor: Editor;
  html: '';

  element: ICourseElement = {name: "", order: 0};
  elementContent: IElementContent = {
    courseElement: {},
    articleContent: {id: "0", text: ""},
    videoContent: {id: "0", videoUrl: ""}
  };
  
  constructor(private courseService: CoursesService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.editor = new Editor();

    this.route.params.subscribe(params => {
      this.element.id = params['elementId'];
    });
    this.courseService.getElementContent(this.element).subscribe(response => {
      this.elementContent = response;
      console.log(this.elementContent);
    })
  }

  ngOnDestroy(): void {
    this.editor.destroy();
  }

  addOrEditElement(){
    this.courseService.setElementContent(this.elementContent, this.element.id).subscribe(response =>{
      this.router.navigateByUrl('/course-edit');
    }, error => {
      console.log(error);
    });
  }

}
