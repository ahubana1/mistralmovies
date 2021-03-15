import { Component, OnInit } from '@angular/core';
import { MovieListComponent } from '../movie-list/movie-list.component';
import { DataAccessService } from '../services/data-access.service';

@Component({
  selector: 'app-show-list',
  templateUrl: '../movie-list/movie-list.component.html',
  styleUrls: ['../movie-list/movie-list.component.css']
})
export class ShowListComponent extends MovieListComponent implements OnInit {
  constructor(public dataAccess: DataAccessService) {
    super(dataAccess)
  }

  ngOnInit() {
    this.populate();

    this.keyupHandler();
  }

  ngOnDestroy() {
    this.keyUpListener.unsubscribe();
  }

  populate(): void {
    this.isLoading = true;

    this.dataAccess.getShows(this.filter).subscribe({
      next: (res) => {
        this.totalItems = res.totalItems;
        this.productions.push(...res.shows);
      },
      complete: () => this.isLoading = false,
    });
  }

}
