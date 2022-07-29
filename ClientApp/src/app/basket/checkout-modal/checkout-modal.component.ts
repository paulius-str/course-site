import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { BasketService } from '../basket.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ICourse } from 'src/app/models/course';

@Component({
  standalone: true,
  selector: 'checkout-modal',
  templateUrl: './checkout-modal.component.html',
  styleUrls: ['./checkout-modal.component.css'],
  imports: [CommonModule, FormsModule],
})
export class CheckoutModalComponent implements OnInit {
  @ViewChild('dialog', { static: true }) dialog!: ElementRef<HTMLDialogElement>;
  courses: ICourse[] = [];


  constructor(public basketService: BasketService, private router: Router) { }

  ngOnInit(): void {
    this.courses = this.basketService.courses;
  }

  closeModal(event: MouseEvent) {
    if (!(event.target as HTMLElement).closest('.modal-custom')) {
      this.router.navigate(['/']);
    }
  }

  proceed(){
    this.router.navigate(['/payment']);
  }

  closeModalSimple() {
      this.router.navigate(['/']);
  }
}
