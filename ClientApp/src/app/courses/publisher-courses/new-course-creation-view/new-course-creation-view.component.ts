import { Component, OnInit } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ICourse } from 'src/app/models/course';
import { ICourseForCreationDto } from 'src/app/models/courseForCreation';
import { CoursesService } from '../../courses.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { Editor } from 'ngx-editor';

@Component({
  selector: 'app-new-course-creation-view',
  templateUrl: './new-course-creation-view.component.html',
  styleUrls: ['./new-course-creation-view.component.css']
})
export class NewCourseCreationViewComponent implements OnInit {
  editor: Editor;
  course: ICourseForCreationDto = {userId: "", name: "", description: "", shortDescription: "", category: "", price: 0};
  myModel: string = '';
  selectedFile: File | null = null;
  fileInputDirty: boolean;
  
  constructor(private coursesService: CoursesService, private router: Router) 
  { 

  }
  
  ngOnInit(): void {
    this.editor = new Editor();
  }

  ngOnDestroy(): void {
    this.editor.destroy();
  }


  onSubmit(form: NgForm) {
    if (form.valid) {
      this.createCourse();
    }
  }

  onFileSelected(event: any) {
    this.fileInputDirty = true;
    this.selectedFile = event.target.files[0] as File;
    console.log('Selected file:', this.selectedFile);
  }

  fileIsValid(){
    if(this.selectedFile !== null)
      return true;

    return false;
  }

  createCourse(){
    if(this.selectedFile == null)
      return;
    
    this.coursesService.createCourseWithImage(this.course, this.selectedFile).subscribe(response => {
      this.router.navigateByUrl('/publisher/courses');
      return this.course;
    });
  }
}
