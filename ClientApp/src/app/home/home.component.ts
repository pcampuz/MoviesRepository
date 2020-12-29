import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  private person: string;
  private allDataResults: models.DetailsMovieVM[] = [];

  constructor(
    private http: HttpClient
  ) {
  }

  SearchPerson(): void {

    this.http.get<models.DetailsMovieVM[]>("api/Home/GetInfoByActorOrDirector", {
      params: {
        person: this.person
      }
    })
      .subscribe((result) => {
        this.allDataResults = result;
      });
  }
}
