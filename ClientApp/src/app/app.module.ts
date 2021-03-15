import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ProductionCardComponent } from './production-card/production-card.component';
import { MovieListComponent } from './movie-list/movie-list.component';
import { RatingModule } from 'ng-starrating';
import { ShowListComponent } from './show-list/show-list.component';
import { ErrorModule } from './error-handling/error.module';
import { SharedModule } from './shared/shared.module';
import { MaterialModule } from './material.module';
import { LimitCharactersPipe } from './pipes/limit-characters.pipe';
import { httpInterceptorProviders } from './request-interceptor';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ProductionCardComponent,
    MovieListComponent,
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
      { path: '', redirectTo:"/movies", pathMatch: 'full' },
      { path: 'movies', component: MovieListComponent },
      { path: 'shows', component: ShowListComponent },
    ]),
    BrowserAnimationsModule
  ],
  providers: [httpInterceptorProviders],
  bootstrap: [AppComponent],
  entryComponents: []
})
export class AppModule { }
