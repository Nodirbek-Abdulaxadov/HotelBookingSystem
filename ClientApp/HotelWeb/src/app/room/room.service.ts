import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class RoomService {

  constructor(private httpClient: HttpClient) { }

  baseUrl: string = "https://localhost:44363/api/roomtypes";

  languge: string = "";

  getAllRooms() {
    this.languge = localStorage.getItem("language")!;
    return this.httpClient.get(this.baseUrl + '/' + this.languge);
  }

  getRoomById(id: number) {
    this.languge = localStorage.getItem("language")!;
    return this.httpClient.get(this.baseUrl+"/" + this.languge + "/"+ id );
  }

  check(form: any): boolean {
    this.httpClient.get('https://localhost:44363/api/room/check?startDate='+form.startDate + '&endDate='+ form.endDate +'&guestsCount='+form.guestCount, {withCredentials: true}).subscribe(
            { next: data => {
                
                  var fail = document.getElementById('unavailability-alert')
                  var success = document.getElementById('availability-alert')
                if (data == true) {
                  if (!fail?.classList.contains('d-none')) {
                    fail?.classList.add('d-none');
                  }
                  success?.classList.remove('d-none');
                }
                else {
                  if (!success?.classList.contains('d-none')) {
                    success?.classList.add('d-none');
                  }
                  fail?.classList.remove('d-none');
                }
            },
            error: error => {
              console.log(error)
            }
        });
    return false;
  }
}
