import { Injectable } from '@angular/core';
import { SearchFilter } from '../models/search-filter';
import { Observable } from 'rxjs';
import { ApiListService } from './api-list.service';
import { HttpHeaders, HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DataAccessService {

  constructor(private apiListService: ApiListService, private http: HttpClient) { }

  public getMovies(filter: SearchFilter): Observable<any> {
    let url: string = this.apiListService.getApiUrl(this.apiListService.getMovies);
    url = url + this.getSearchQuery(filter);

    var httpOptions = {
      headers: new HttpHeaders().set('content-type', 'application/json'),
    };

    return this.http.get(url, httpOptions);
  }

  public getShows(filter: SearchFilter): Observable<any> {
    let url: string = this.apiListService.getApiUrl(this.apiListService.getShows);
    url = url + this.getSearchQuery(filter);

    var httpOptions = {
      headers: new HttpHeaders().set('content-type', 'application/json'),
    };

    return this.http.get(url, httpOptions);
  }

  private getSearchQuery(filter:SearchFilter) {
    let query:string = "";
    query = query.concat("?");
    if(filter.searchTerm.length > 0) query = query.concat("q=", filter.searchTerm, "&");
    query = query.concat("page=", filter.page.toString(), "&");
    query = query.concat("count=", filter.count.toString());

    return query;
  }
}
