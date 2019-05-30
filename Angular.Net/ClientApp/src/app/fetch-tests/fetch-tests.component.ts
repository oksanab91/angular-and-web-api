import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { TraineeTestFetch } from '../models/trainee-test-fetch';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-fetch-tests',
  templateUrl: './fetch-tests.component.html',
  styleUrls: ['./fetch-tests.component.css']
})
export class FetchTestsComponent implements OnInit {  
  baseUrl = 'https://localhost:44397';
  traineeId: number;
  traineeTests: TraineeTestFetch[];
  traineeName = '';
  header = '';
  getUrl = '';

  constructor(private http: HttpClient, route: ActivatedRoute, private location: Location) {
    this.traineeId = + route.snapshot.paramMap.get('traineeId');
    this.traineeName = route.snapshot.paramMap.get('traineeName');

    console.log('trainee id, name: ', this.traineeId, this.traineeName);

    this.header = 'Trainee Tests Results';
    if(this.traineeName){
      this.header += ' for ' + this.traineeName;
    }    

    //https://localhost:44397/api/traineeTests/1
    
    if(this.traineeId > 0){
      this.getUrl = this.baseUrl + '/api/traineeTests/' + this.traineeId;
    }else{
      this.getUrl = this.baseUrl + '/api/traineeTests';
    }
    console.log(this.getUrl);

    this.getTests();      
  }

  removeTest(traineeTestId: number): void {
    console.log(traineeTestId);

    if(traineeTestId){
      this.http.delete<number>(`${this.baseUrl}/api/TraineeTests/${traineeTestId}`).subscribe(result => {
        console.log("Delete Trainee Test ", result);
        alert("The Test Removed");
        
        this.getTests();

      }, error => console.log(error));
    }
  }

  ngOnInit() {
  }

  getTests(){
    this.http.get<TraineeTestFetch[]>(this.getUrl).subscribe(result => {
      console.log("FetchTestsComponent", result);
      this.traineeTests = result;

    }, (error: HttpErrorResponse) => {
      console.log(error);
      if (error.status == 404){
        return;
      }
    });
  }

  goBack(): void {
    this.location.back();    
  }
}