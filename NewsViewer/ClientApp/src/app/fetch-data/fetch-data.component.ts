import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: News[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<News[]>(baseUrl + 'weatherforecast').subscribe(result => {
      this.forecasts = result;
      console.log(result);
    }, error => console.error(error));
  }
}

interface News {
  NewsTitle: string;
}
