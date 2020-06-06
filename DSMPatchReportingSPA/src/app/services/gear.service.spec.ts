/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { GearService } from './gear.service';

describe('Service: Gear', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [GearService]
    });
  });

  it('should ...', inject([GearService], (service: GearService) => {
    expect(service).toBeTruthy();
  }));
});
