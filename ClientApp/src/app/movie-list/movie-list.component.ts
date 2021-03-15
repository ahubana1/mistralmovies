import { Component, OnInit, OnDestroy, ViewChild, ElementRef } from '@angular/core';
import { DataAccessService } from '../services/data-access.service';
import { SearchFilter } from '../models/search-filter';
import { Movie } from '../models/movie-model';
import { Subscription, fromEvent } from 'rxjs';
import { map, debounceTime, distinctUntilChanged, filter } from 'rxjs/operators';

@Component({
  selector: 'app-movie-list',
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.css']
})
export class MovieListComponent implements OnInit, OnDestroy {
  @ViewChild('userSearchInput', { static: true }) userSearchInput: ElementRef;
  isLoading: boolean = false;
  totalItems: number = 0;
  movies: Movie[] = [];
  filter: SearchFilter = {
    searchTerm: "",
    page: 1,
    count: 10
  };
  keyUpListener: Subscription;

  constructor(private dataAccess: DataAccessService) { }

  ngOnInit() {
    this.populateMovies();

    this.keyUpListener = fromEvent(this.userSearchInput.nativeElement, "keyup")
      .pipe(
        map((event: any) => {
          return event.target.value;
        }),
        filter(res => res.length != 1), //at least 2 or when cleared - hence != 0
        debounceTime(500),
        distinctUntilChanged()
      )
      .subscribe((text: string) => {
        this.resetFilter(text);
        this.populateMovies();
      });
  }

  ngOnDestroy() {
    this.keyUpListener.unsubscribe();
  }

  populateMovies(): void {
    this.isLoading = true;

    this.dataAccess.getMovies(this.filter).subscribe({
      next: (res) => {
        this.totalItems = res.totalItems;
        // this.movies = res.movies;
        this.movies.push(...res.movies);
      },
      complete: () => this.isLoading = false,
    });
  }

  clear():void {
    this.resetFilter();
    this.populateMovies();
  }

  loadMore():void {
    if(this.filter.page * this.filter.count < this.totalItems) {
      this.filter.page++;
      this.populateMovies();
    }
  }

  resetFilter(text?:string):void {
    this.movies = [];
    this.filter = {
      searchTerm: text ? text : "",
      page: 1,
      count: 10
    }
  }
}
