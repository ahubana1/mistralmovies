import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MovieCardComponent } from './movie-card/movie-card.component';
import { MovieListComponent } from './movie-list/movie-list.component';
import { RatingModule } from 'ng-starrating';
import { LoaderComponent } from './shared/loader/loader.component';
import { ShowListComponent } from './show-list/show-list.component';
import { ErrorModule } from './error-handling/error.module';
import { SharedModule } from './shared/shared.module';
import { MaterialModule } from './material.module';
import { LimitCharactersPipe } from './pipes/limit-characters.pipe';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    MovieCardComponent,
    MovieListComponent,
    LoaderComponent,
    ShowListComponent,
    LimitCharactersPipe,

  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    MaterialModule,
    ErrorModule,
    SharedModule,
    RatingModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'movies', component: MovieListComponent },
      { path: 'shows', component: ShowListComponent },
    ]),
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent],
  entryComponents: []
})
export class AppModule { }
