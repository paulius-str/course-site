import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
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
import { JwtInterceptor } from './util/jwt-interceptor';
import { NgxEditorModule } from 'ngx-editor';
import { CheckoutModalComponent } from './basket/checkout-modal/checkout-modal.component';
import { PaymentModalComponent } from './basket/payment-modal/payment-modal.component';
import { EditSectionViewComponent } from './courses/publisher-courses/course-editing-view/edit-section-view/edit-section-view.component';

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
    EditSectionViewComponent,
    EditElementViewComponent,
    VideoPlayerComponent,
    RatingComponent,
    CreateEditRatingViewComponent,
    QuestionComponent,
    QuestionViewComponent,
    AnswerComponent,
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
    NgxEditorModule,
    FormsModule,
    YouTubePlayerModule,
    FormsModule,
    NgbModule,
    ReactiveFormsModule,
    NotifierModule,
    RouterModule.forRoot([
      { path: '', redirectTo: '/home', pathMatch: 'full' },
      { path: 'home', component: HomeComponent, pathMatch: 'full', title: 'Home' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent, title: 'Home' },
      { path: 'login', component: LoginComponent, title: 'Login' },
      { path: 'signup', component: RegisterComponent, title: 'Register' },
      { path: 'courses', component: MycoursesComponent, title: 'Courses' },
      { path: 'course', component: CourseViewComponent, title: 'Course' },
      { path: 'course/create-rating/:courseId', component: CreateEditRatingViewComponent, title: 'Create Rating' },
      { path: 'course/element', component: ElementViewComponent, title: 'Course Content' },
      { path: 'publisher/courses', component: PublisherCoursesComponent, title: 'Created Courses' },
      { path: 'course-edit', component: CourseEditingViewComponent, title: 'Edit Course' },
      { path: 'question', component: QuestionViewComponent, title: 'Discussion' },
      { path: 'publisher/courses/new-course', component: NewCourseCreationViewComponent, title: 'New Course' },
      { path: 'course-edit/new-section', component: NewSectionViewComponent, title: 'New Section' },
      { path: 'course-edit/new-element/:sectionId', component: NewElementViewComponent, title: 'New Element' },
      { path: 'course-edit/edit-element/:elementId', component: EditElementViewComponent, title: 'Edit Element Content' },
      { path: 'course-edit/edit-section', component: EditElementViewComponent, title: 'Edit Section' },
      { path: 'checkout', component: CheckoutModalComponent, title: 'Checkout' },
      { path: 'payment', component: PaymentModalComponent, title: 'Payment' },
    ]),
    
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
