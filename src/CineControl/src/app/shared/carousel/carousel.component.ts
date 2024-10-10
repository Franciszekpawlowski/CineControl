import { Component, Input, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon'; // Import MatIconModule

@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.scss'],
  standalone: true,
  imports: [CommonModule, MatIconModule], // Dodaj MatIconModule do importów
})
export class CarouselComponent implements OnInit, OnDestroy {
  @Input() items: any[] = [];
  @Input() itemTemplate!: any;
  @Input() autoPlay = false;
  @Input() autoPlayInterval = 5000;

  currentIndex = 0;
  autoPlayIntervalId: any;

  ngOnInit() {
    if (this.autoPlay) {
      this.startAutoPlay();
    }
  }

  ngOnDestroy() {
    this.stopAutoPlay();
  }

  next() {
    this.currentIndex = (this.currentIndex + 1) % this.items.length;
  }

  prev() {
    this.currentIndex = (this.currentIndex - 1 + this.items.length) % this.items.length;
  }

  selectSlide(index: number) {
    this.currentIndex = index;
    this.resetAutoPlay();
  }

  startAutoPlay() {
    this.autoPlayIntervalId = setInterval(() => {
      this.next();
    }, this.autoPlayInterval);
  }

  stopAutoPlay() {
    if (this.autoPlayIntervalId) {
      clearInterval(this.autoPlayIntervalId);
    }
  }

  resetAutoPlay() {
    if (this.autoPlay) {
      this.stopAutoPlay();
      this.startAutoPlay();
    }
  }
}
