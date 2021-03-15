import { Component, OnInit, Input } from '@angular/core';
import { StarRatingComponent } from 'ng-starrating';
import { VideographyProduction } from '../models/movie-model';
import { RatingsService } from '../services/ratings.service';
import { RateViewModel } from '../models/rate-model';
import { Router } from '@angular/router';

@Component({
  selector: 'production-card',
  templateUrl: './production-card.component.html',
  styleUrls: ['./production-card.component.css']
})
export class ProductionCardComponent implements OnInit {
  @Input() production:VideographyProduction;

  constructor(private ratingService:RatingsService, private router:Router) { }

  ngOnInit() {
  }

  rate($event:{oldValue:number, newValue:number, starRating:StarRatingComponent}, movieId:string) {
    let rvm = new RateViewModel();
    rvm.id = movieId;
    rvm.rate = $event.newValue;

    let isShow = this.router.url.includes("shows");

    this.ratingService.rate(rvm, isShow).subscribe({
      next: res => console.log("rated")
    });
  }

}
