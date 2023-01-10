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
}
