import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TraineeShort } from '../models/trainee';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-edit-trainee',
  templateUrl: './edit-trainee.component.html',
  styleUrls: ['./edit-trainee.component.css']
})
export class EditTraineeComponent implements OnInit {
  baseUrl = 'https://localhost:44397';
  trainee: TraineeShort;  
  traineeId: number; 
  
  constructor(private http: HttpClient, route: ActivatedRoute, private location: Location) {        
    this.traineeId = + route.snapshot.paramMap.get('traineeId');   
  
    this.http.get<TraineeShort>(this.baseUrl + '/api/trainees/' + this.traineeId).subscribe(result => {
      console.log("Get Trainee ", result);
      this.trainee = result;
    }, error => console.log(error));
  }

  updateTrainee(): void {
    if(this.trainee){
      this.http.put<TraineeShort>(`${this.baseUrl}/api/trainees/${this.traineeId}`, this.trainee).subscribe(result => {
        console.log("Save Trainee ", result);
        
      }, error => console.log(error));
    }
  }

  goBack(): void {
    this.location.back();
  }

  ngOnInit() {
  }

}