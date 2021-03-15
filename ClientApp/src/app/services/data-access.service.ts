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

    url = url.concat("?");
    if(filter.searchTerm.length > 0) url = url.concat("q=", filter.searchTerm, "&");
    url = url.concat("page=", filter.page.toString(), "&");
    url = url.concat("count=", filter.count.toString());

    var httpOptions = {
      headers: new HttpHeaders().set('content-type', 'application/json'),
    };

    return this.http.get(url, httpOptions);
  }
}
