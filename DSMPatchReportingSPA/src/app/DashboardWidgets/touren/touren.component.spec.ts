/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { TourenComponent } from './touren.component';

describe('TourenComponent', () => {
  let component: TourenComponent;
  let fixture: ComponentFixture<TourenComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TourenComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TourenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
