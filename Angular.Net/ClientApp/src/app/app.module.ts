import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchTraineesComponent } from './fetch-trainees/fetch-trainees.component';
import { FetchTestsComponent } from './fetch-tests/fetch-tests.component';
import { EditTraineeComponent } from './edit-trainee/edit-trainee.component';
import { AddTraineeComponent } from './add-trainee/add-trainee.component';
import { RemoveTraineeComponent } from './remove-trainee/remove-trainee.component';
import { AddTraineeTestComponent } from './add-trainee-test/add-trainee-test.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchTraineesComponent,
    FetchTestsComponent,
    EditTraineeComponent,
    AddTraineeComponent,
    RemoveTraineeComponent,
    AddTraineeTestComponent    
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-trainees', component: FetchTraineesComponent },      
      { path: 'fetch-tests/:traineeId/:traineeName', component: FetchTestsComponent},
      { path: 'fetch-tests', component: FetchTestsComponent},      
      { path: 'edit-trainee/:traineeId', component: EditTraineeComponent},
      { path: 'remove-trainee/:traineeId', component: RemoveTraineeComponent},
      { path: 'add-trainee', component: AddTraineeComponent},      
      { path: 'add-trainee-test/:traineeId/:traineeName', component: AddTraineeTestComponent}
    ]),    
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
