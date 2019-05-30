import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddTraineeTestComponent } from './add-trainee-test.component';

describe('AddTraineeTestComponent', () => {
  let component: AddTraineeTestComponent;
  let fixture: ComponentFixture<AddTraineeTestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddTraineeTestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddTraineeTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
