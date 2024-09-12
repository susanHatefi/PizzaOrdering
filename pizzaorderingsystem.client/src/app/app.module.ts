import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule, isDevMode } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ShareModule } from './share/share.module';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { WelcomeComponent } from './component/welcome/welcome.component';
import { GuideComponent } from './component/guide/guide.component';
import { AppReducer } from './state-management/reducer';
import { LoadingInterceptor } from './core/core-reference';
import { SpinnerModule } from './share/controls/spinner/spinner.module';

@NgModule({
  declarations: [AppComponent, WelcomeComponent, GuideComponent],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    ShareModule,
    AppRoutingModule,
    StoreModule.forRoot({ app: AppReducer }, {}),
    EffectsModule.forRoot([]),
    StoreDevtoolsModule.instrument({ maxAge: 25, logOnly: !isDevMode() }),
    SpinnerModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
