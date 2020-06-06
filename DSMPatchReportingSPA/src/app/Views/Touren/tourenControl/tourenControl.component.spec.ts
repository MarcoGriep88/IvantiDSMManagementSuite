/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { TourenControlComponent } from './tourenControl.component';

describe('TourenControlComponent', () => {
  let component: TourenControlComponent;
  let fixture: ComponentFixture<TourenControlComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TourenControlComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TourenControlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
