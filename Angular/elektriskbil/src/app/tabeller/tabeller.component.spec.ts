import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TabellerComponent } from './tabeller.component';

describe('TabellerComponent', () => {
  let component: TabellerComponent;
  let fixture: ComponentFixture<TabellerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TabellerComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TabellerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
