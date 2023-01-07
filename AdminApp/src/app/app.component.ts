import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  ngOnInit(): void {
    const body = document.querySelector('body')!,
      sidebar = body.querySelector('nav')!,
      toggle = body.querySelector(".toggle")!,
      home = body.querySelector(".toggle")!,
      modeSwitch = body.querySelector(".toggle-switch")!;
      toggle.addEventListener("click" , () => {
          sidebar.classList.toggle("close");
      });

      modeSwitch.addEventListener("click" , () =>{
          body.classList.toggle("dark");
      });
  }
  title = 'Hotel Admin';
}
