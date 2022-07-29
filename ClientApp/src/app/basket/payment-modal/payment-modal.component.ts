import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { BasketService } from '../basket.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ICourse } from 'src/app/models/course';

@Component({
  standalone: true,
  selector: 'payment-modal',
  templateUrl: './payment-modal.component.html',
  styleUrls: ['./payment-modal.component.css'],
  imports: [CommonModule, FormsModule],
})
export class PaymentModalComponent implements OnInit {
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

  closeModalSimple() {
      this.router.navigate(['/']);
  }

  checkout() {
    this.basketService.checkout();
    this.router.navigate(['/']);
  }
}
