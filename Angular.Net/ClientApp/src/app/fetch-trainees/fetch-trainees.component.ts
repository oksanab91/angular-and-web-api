import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TraineeShort } from '../models/trainee';
import { Location } from '@angular/common';

@Component({
  selector: 'app-fetch-trainees',
  templateUrl: './fetch-trainees.component.html'
})
export class FetchTraineesComponent {  
  public trainees: TraineeShort[];  
  baseUrl = 'https://localhost:44397'; 
  
  constructor(private http: HttpClient, private location: Location) { 
    this.http.get<TraineeShort[]>(this.baseUrl + '/api/trainees').subscribe(result => {
      console.log("FetchDataComponent", result);
      this.trainees = result;
    }, error => console.error(error));
  }
 
  goBack(): void {
    this.location.back();    
  }
}