import { Component, OnInit } from '@angular/core';
import { TraineeShort } from '../models/trainee';
import { HttpClient } from '@angular/common/http';
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-remove-trainee',
  templateUrl: './remove-trainee.component.html',
  styleUrls: ['./remove-trainee.component.css']
})
export class RemoveTraineeComponent implements OnInit {
  baseUrl = 'https://localhost:44397';
  trainee: TraineeShort;
  traineeId = 0;
    
  constructor(private http: HttpClient, private location: Location, private route: ActivatedRoute) {
    this.traineeId = + route.snapshot.paramMap.get('traineeId');  
  
    this.http.get<TraineeShort>(this.baseUrl + '/api/trainees/' + this.traineeId).subscribe(result => {
      console.log("Get Trainee ", result);
      this.trainee = result;
    }, error => console.log(error));
   }

  removeTrainee(): void {
    if(this.trainee){
      this.http.delete<TraineeShort>(`${this.baseUrl}/api/trainees/${this.trainee.traineeId}`).subscribe(result => {
        console.log("Delete Trainee ", result);

        this.location.back();
      
      }, error => console.log(error));
    }
  }

  goBack(): void {
    this.location.back();
  }
  
  ngOnInit() {
  }

}