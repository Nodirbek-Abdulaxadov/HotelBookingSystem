import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  baseUrl = "https://localhost:44363/api/orders/";

  constructor(private httpClient: HttpClient) { }

  email: string = ""
  createOrder(form: any, roomId: number) {

    this.email = (JSON.parse(localStorage.getItem("data")??""))["Email"].replace('@', "%40");
    
    this.httpClient.get(this.baseUrl + "check?email="+this.email+"&roomTypeId="+roomId).subscribe({
      next: data => {
        if (data == true){
          this.httpClient.post(this.baseUrl + "create/" + this.email, form).subscribe({
            next: data => {
              alert('Order created successfully!');
            },
            error: error =>{
              console.log(error);
            }
          })}
          else {
            var el = document.getElementById('waiting')!;
            el.style.display = "none";
            el = document.getElementById("error")!;
            el.innerHTML = "Sorry, we coud not found empty room from your choise!\nPlease, choose another room!"
            el.style.display = "block";
          }
      }, 
      error: error =>{
        console.log(error);
      }
    });
  }
}
