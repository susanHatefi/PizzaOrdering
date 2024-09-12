import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MainState, Selectors, ToppingEnum } from './reference';
import { Store } from '@ngrx/store';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  loading$ = this.store.select(Selectors.getSpinner);
  constructor(private store: Store<MainState>) {}
  ngOnInit() {}

  title = 'pizzaorderingsystem.client';
}
