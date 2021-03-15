import { Component, OnInit, Input } from '@angular/core';
import { StarRatingComponent } from 'ng-starrating';
import { Movie } from '../models/movie-model';
import { RatingsService } from '../services/ratings.service';
import { RateViewModel } from '../models/rate-model';

@Component({
  selector: 'movie-card',
  templateUrl: './movie-card.component.html',
  styleUrls: ['./movie-card.component.css']
})
export class MovieCardComponent implements OnInit {
  @Input() movie:Movie;

  constructor(private ratingService:RatingsService) { }

  ngOnInit() {
  }

  rate($event:{oldValue:number, newValue:number, starRating:StarRatingComponent}, movieId:string) {
    let rvm = new RateViewModel();
    rvm.id = movieId;
    rvm.rate = $event.newValue;

    this.ratingService.rate(rvm).subscribe({
      next: res => console.log("rated")
    });
  }

}
