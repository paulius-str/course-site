import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CourseComponent } from './courses/course/course.component';
import { CoursesService } from './courses/courses.service';
import { NotifierModule } from 'angular-notifier';
import { JwtModule } from '@auth0/angular-jwt';
import { MycoursesComponent } from './courses/mycourses/mycourses.component';
import { ShopComponent } from './courses/shop/shop.component';
import { BasketComponent } from './basket/basket.component';
import { CourseViewComponent } from './courses/course-view/course-view.component';
import { ElementViewComponent } from './courses/element-view/element-view.component';
import { PublisherCoursesComponent } from './courses/publisher-courses/publisher-courses.component';
import { NewCourseCreationViewComponent } from './courses/publisher-courses/new-course-creation-view/new-course-creation-view.component';
import { CourseEditingViewComponent } from './courses/publisher-courses/course-editing-view/course-editing-view.component';
import { NewSectionViewComponent } from './courses/publisher-courses/course-editing-view/new-section-view/new-section-view.component';
import { NewElementViewComponent } from './courses/publisher-courses/course-editing-view/new-element-view/new-element-view.component';
import { EditElementViewComponent } from './courses/publisher-courses/course-editing-view/edit-element-view/edit-element-view.component';
import { YouTubePlayerModule } from '@angular/youtube-player';
import { VideoPlayerComponent } from './video-player/video-player.component';
import { RatingComponent } from './ratings/rating/rating.component';
import { CreateEditRatingViewComponent } from './ratings/create-edit-rating-view/create-edit-rating-view.component';
import { QuestionComponent } from './discussion/question/question.component';
import { QuestionViewComponent } from './discussion/question-view/question-view.component';
import { AnswerComponent } from './discussion/answer/answer.component';

export function tokenGetter(){
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginComponent,
    RegisterComponent,
    CourseComponent,
    ShopComponent,
    MycoursesComponent,
    BasketComponent,
    CourseViewComponent,
    ElementViewComponent,
    PublisherCoursesComponent,
    NewCourseCreationViewComponent,
    CourseEditingViewComponent,
    NewSectionViewComponent,
    NewElementViewComponent,
    EditElementViewComponent,
    VideoPlayerComponent,
    RatingComponent,
    CreateEditRatingViewComponent,
    QuestionComponent,
    QuestionViewComponent,
    AnswerComponent 
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["example.com"],
        disallowedRoutes: ["http://example.com/examplebadroute/"],
      },
    }),
    YouTubePlayerModule,
    FormsModule,
    NgbModule,
    ReactiveFormsModule,
    NotifierModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'login', component: LoginComponent },
      { path: 'signup', component: RegisterComponent },
      { path: 'courses', component: MycoursesComponent },
      { path: 'course', component: CourseViewComponent },
      { path: 'course/element', component: ElementViewComponent },
      { path: 'publisher/courses', component: PublisherCoursesComponent },
      { path: 'course-edit', component: CourseEditingViewComponent },
      { path: 'question', component: QuestionViewComponent }
    ]),
    
  ],
  providers: [
    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
