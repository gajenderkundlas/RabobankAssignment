import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-show-portfolio',
  templateUrl: './show-portfolio.component.html',
  styleUrls: ['./show-portfolio.component.css']
})
export class ShowPortfolioComponent implements OnInit {
  public portFolio: portfolio[];
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<portfolio[]>(baseUrl + 'api/Portfolio/Get').subscribe((result:any) => {
      if (result != undefined) {
        this.portFolio = result.positions;
      }
    }, error => console.error(error));
  }

  ngOnInit() {

  }


}

interface portfolio {
  code: string;
  name: string;
  value: number;
  mandates: any[];
}
