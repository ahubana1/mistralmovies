import { Injectable } from '@angular/core';
import { ApiListService } from './api-list.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RateViewModel } from '../models/rate-model';

@Injectable({
  providedIn: 'root'
})
export class RatingsService {

  constructor(private apiListService: ApiListService, private http: HttpClient) { }

  public rate(rateViewModel:RateViewModel, isShow?:boolean): Observable<any> {
    let url: string = this.apiListService.getApiUrl(this.apiListService.rateMovie);
    if(isShow) url = this.apiListService.getApiUrl(this.apiListService.rateShow);

    var httpOptions = {
      headers: new HttpHeaders().set('content-type', 'application/json'),
      body: rateViewModel
    };

    return this.http.post(url, httpOptions.body, httpOptions);
  }
}
