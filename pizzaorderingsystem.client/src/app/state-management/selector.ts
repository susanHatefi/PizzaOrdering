import { createFeatureSelector, createSelector } from '@ngrx/store';
import { AppState } from './state';

const appSelector = createFeatureSelector<AppState>('app');

export const getSpinner = createSelector(appSelector, (state) => {
  return state?.toggleSpinner;
});

export const getError = createSelector(appSelector, (state) => {
  return state?.error;
});
