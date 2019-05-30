import { Component, OnInit } from '@angular/core';
import { TestShort } from '../models/test-short';
import { SubjectShort } from '../models/subject-short';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { TraineeTestCreateFill } from '../models/trainee-test-create-fill';
import { TraineeTestCreate } from '../models/trainee-test-create';

@Component({
  selector: 'app-add-trainee-test',
  templateUrl: './add-trainee-test.component.html',
  styleUrls: ['./add-trainee-test.component.css']
})
export class AddTraineeTestComponent implements OnInit {  
  traineeId: number;
  traineeName = '';
  header = 'Add Test';
  baseUrl = 'https://localhost:44397';
  
  traineeTestFill: TraineeTestCreateFill;
  traineeTest = new TraineeTestCreate();
  tests: TestShort[];
  subjects: SubjectShort[];
  statuses: ['Pass', 'Failed'];  

  constructor(private http: HttpClient, private location: Location, private route: ActivatedRoute, private router: Router) { 
    this.header = 'Add Test Result';
    this.traineeId = + this.route.snapshot.paramMap.get('traineeId');
    this.traineeName = this.route.snapshot.paramMap.get('traineeName');

    if(this.traineeName){
      this.header += ' for ' + this.traineeName;
    }

    if(this.traineeId>0){
      this.http.get<TraineeTestCreateFill>(`${this.baseUrl}/api/TraineeTestCreate/${this.traineeId}`).subscribe(result => {
        console.log("Get TraineeTestCreate ", result);
        this.traineeTestFill = result;
        this.traineeTest.traineeId = this.traineeId;     

      }, error => console.log(error));
    }
  }

  addTest(form){    
    console.log(this.traineeTest);

    if(this.traineeTest.testId>0){
      this.http.post<TraineeTestCreate>(`${this.baseUrl}/api/TraineeTestCreate`, this.traineeTest).subscribe(result => {
        console.log("Add TraineeTestCreate ", result);        

        alert("The form was submitted");
        form.reset();
        
      }, error => console.log(error));
    }
  }

  goBack(): void {
    console.log(this.location);    
    this.router.navigate(['/fetch-tests', this.traineeId, this.traineeName]);  //['/fetch-tests/3/Rita%20F']
  }

  ngOnInit() {
  }
}
