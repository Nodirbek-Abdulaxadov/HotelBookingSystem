import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(public translate: TranslateService) {
    translate.addLangs(['en', 'uz', 'ru']);
    var localLang = localStorage.getItem("language");
    if (localLang) {
      translate.use(localLang);
    }
    else {
      translate.use('en');
    }
  }
}
