import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseUrl } from 'src/app/models/BaseUrl';
import { RoomType } from 'src/app/models/RoomType';

@Injectable({
  providedIn: 'root'
})
export class RoomTypeService {

  constructor(private httpClient: HttpClient) {
  }

  url: string = "https://localhost:44363/api/RoomTypes/";

  getAll(): Observable<RoomType[]> {
    return this.httpClient.get<RoomType[]>(this.url);
  }

  get(id: number): Observable<RoomType> {
    return this.httpClient.get<RoomType>(this.url + id);
  }
}
