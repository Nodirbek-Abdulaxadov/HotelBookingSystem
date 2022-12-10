import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RoomModel } from '../interfaces/RoomModel';

@Injectable({
  providedIn: 'root'
})
export class RoomService {

  constructor(private httpClient: HttpClient) { }

  baseUrl: string = "https://localhost:44363/api/rooms/";

  getAllRooms() {
    return this.httpClient.get(this.baseUrl);
  }

  getRoomById(id: number) {
    return this.httpClient.get(this.baseUrl + id);
  }
}
