import { Component, OnInit } from '@angular/core';
import { Promotion } from '../../models/promotion.model';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CarouselComponent } from '../carousel/carousel.component';

@Component({
  selector: 'app-promotions',
  templateUrl: './promotions.component.html',
  styleUrls: ['./promotions.component.scss'],
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatIconModule, CarouselComponent],
})
export class PromotionsComponent implements OnInit {
  promotions: Promotion[] = [
    {
      id: 1,
      title: 'Promocja 1',
      description: 'Opis promocji 1',
      imageUrl: 'images/promo1.png',
      ctaText: 'Sprawdź',
      link: '/promotions/1',
    },
    {
      id: 2,
      title: 'Promocja 2',
      description: 'Opis promocji 2',
      imageUrl: 'images/promo2.png',
      ctaText: 'Dowiedz się więcej',
      link: '/promotions/2',
    },
  ];

  ngOnInit() {}
}
