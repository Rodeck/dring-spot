import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MeetingPlaceAddComponent } from './meeting-place-add.component';

describe('MeetingPlaceAddComponent', () => {
  let component: MeetingPlaceAddComponent;
  let fixture: ComponentFixture<MeetingPlaceAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MeetingPlaceAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MeetingPlaceAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
