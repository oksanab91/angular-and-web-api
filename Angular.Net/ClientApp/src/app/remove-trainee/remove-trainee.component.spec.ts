import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RemoveTraineeComponent } from './remove-trainee.component';

describe('DeleteTraineeComponent', () => {
  let component: RemoveTraineeComponent;
  let fixture: ComponentFixture<RemoveTraineeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RemoveTraineeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RemoveTraineeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
