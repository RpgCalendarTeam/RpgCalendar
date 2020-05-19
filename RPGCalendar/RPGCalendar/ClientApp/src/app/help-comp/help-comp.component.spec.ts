import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HelpCompComponent } from './help-comp.component';

describe('HelpCompComponent', () => {
  let component: HelpCompComponent;
  let fixture: ComponentFixture<HelpCompComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HelpCompComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HelpCompComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
