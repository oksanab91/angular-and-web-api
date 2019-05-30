import { Component, OnInit } from '@angular/core';
import { TraineeShort } from '../models/trainee';
import { HttpClient } from '@angular/common/http';
import { Location } from '@angular/common';

@Component({
  selector: 'app-add-trainee',
  templateUrl: './add-trainee.component.html',
  styleUrls: ['./add-trainee.component.css']
})
export class AddTraineeComponent implements OnInit {
  trainee = new TraineeShort();
  baseUrl = 'https://localhost:44397';

  constructor(private http: HttpClient, private location: Location) { }

  ngOnInit() {
  }

  addTrainee(): void {
    console.log(this.trainee);
    if(this.trainee){
      this.http.post<TraineeShort>(`${this.baseUrl}/api/trainees`, this.trainee).subscribe(result => {
        console.log("Add Trainee ", result);
        this.trainee = result;

        this.location.back();
        
      }, error => console.log(error));
    }
  }

  goBack(): void {    
    this.location.back();
  }
}
