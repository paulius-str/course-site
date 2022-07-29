import { ChangeDetectorRef, Component, ElementRef, NgModule, OnInit, Renderer2, ViewChild, inject } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, FormsModule, NgForm, ReactiveFormsModule, Validators }   from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  imports: [CommonModule, FormsModule],
})

export class LoginComponent implements OnInit{
  email: string = "";
  password: string = "";
  @ViewChild('dialog', { static: true }) dialog!: ElementRef<HTMLDialogElement>;
  authService = inject(AuthService);
  router = inject(Router);
  
  ngOnInit(): void {
    this.dialog.nativeElement.showModal();
  }

  onSubmit(form: NgForm) {
    if (form.valid) {
      this.login();
    }
  }

  login(){
    var result = this.authService.login({emailAddress: this.email, password: this.password});
    this.router.navigate(['/']);
  }
  
  closeModal(event: MouseEvent) {
    if (!(event.target as HTMLElement).closest('.modal-custom')) {
      this.router.navigate(['/']);
    }
  }
  
  navigateToSignup() {
    this.router.navigate(['/signup']);
  }
}
