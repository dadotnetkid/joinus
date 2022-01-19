import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ApiModule } from 'src/api/api.module';
import { environment } from 'src/environments/environment';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FilesModule } from './pages/files/files.module';
import { UiModule } from './ui.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ApiModule.forRoot({rootUrl: environment.apiUrl}),
    AppRoutingModule,
    UiModule,
    FilesModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
