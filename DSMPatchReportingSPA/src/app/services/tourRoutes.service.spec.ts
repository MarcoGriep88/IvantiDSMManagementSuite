/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { TourRoutesService } from './tourRoutes.service';

describe('Service: TourRoutes', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TourRoutesService]
    });
  });

  it('should ...', inject([TourRoutesService], (service: TourRoutesService) => {
    expect(service).toBeTruthy();
  }));
});
