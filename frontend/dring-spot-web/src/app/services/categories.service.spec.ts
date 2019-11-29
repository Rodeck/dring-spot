import { TestBed } from '@angular/core/testing';

import { MeetingPlaceService } from './categories.service';

describe('CategoriesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MeetingPlaceService = TestBed.get(MeetingPlaceService);
    expect(service).toBeTruthy();
  });
});
