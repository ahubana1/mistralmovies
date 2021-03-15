import { Component, OnInit, OnDestroy, ViewChild, ElementRef } from '@angular/core';
import { DataAccessService } from '../services/data-access.service';
import { SearchFilter } from '../models/search-filter';
import { VideographyProduction } from '../models/movie-model';
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
  productions: VideographyProduction[] = [];
  filter: SearchFilter = {
    searchTerm: "",
    page: 1,
    count: 10
  };
  keyUpListener: Subscription;

  constructor(public dataAccess: DataAccessService) { }

  ngOnInit() {
    this.populate();

    this.keyupHandler();
  }

  ngOnDestroy() {
    this.keyUpListener.unsubscribe();
  }

  populate(): void {
    this.isLoading = true;

    this.dataAccess.getMovies(this.filter).subscribe({
      next: (res) => {
        this.totalItems = res.totalItems;
        this.productions.push(...res.movies);
      },
      complete: () => this.isLoading = false,
    });
  }

  clear():void {
    this.resetFilter();
    this.populate();
  }

  loadMore():void {
    if(this.filter.page * this.filter.count < this.totalItems) {
      this.filter.page++;
      this.populate();
    }
  }

  resetFilter(text?:string):void {
    this.productions = [];
    this.filter = {
      searchTerm: text ? text : "",
      page: 1,
      count: 10
    }
  }

  keyupHandler() {
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
      this.populate();
    });
  }
}
