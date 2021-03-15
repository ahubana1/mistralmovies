import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiListService {
  public localBaseApiUrl: string = "https://localhost:44393/api";
  public cloudBaseApiUrl: string = "https://mistralmovies.azurewebsites.net/api";

  public getMovies: string = "/search/movies";
  public getShows: string = "/search/shows";
  public rateMovie: string = "/rate/movie";
  public rateShow: string = "/rate/show";

  constructor() { }

  getApiUrl(url: string) { 
    return this.cloudBaseApiUrl + url;
  }
}
