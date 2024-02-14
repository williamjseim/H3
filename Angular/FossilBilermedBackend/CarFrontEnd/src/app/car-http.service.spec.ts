import { TestBed } from '@angular/core/testing';

import { CarHttpService } from './car-http.service';

describe('CarHttpService', () => {
  let service: CarHttpService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CarHttpService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
