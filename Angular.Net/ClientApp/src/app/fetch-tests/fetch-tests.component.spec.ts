import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FetchTestsComponent } from './fetch-tests.component';

describe('FetchTestsComponent', () => {
  let component: FetchTestsComponent;
  let fixture: ComponentFixture<FetchTestsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FetchTestsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FetchTestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
