import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
    constructor(public translate: TranslateService){
    }
  ngOnInit(): void {
    var lang = localStorage.getItem('language')!;
    var el = document.getElementById(lang)!;
    el.setAttribute("selected", "");
  }
    
    public changeLanguage(event: any) {
      var lang = event.target.value;
      this.translate.use(lang);
      localStorage.setItem('language', lang);
  }  
}
