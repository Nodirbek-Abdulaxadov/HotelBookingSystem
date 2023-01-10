import { Component, OnInit } from '@angular/core';
import { RoomTypeModel } from '../interfaces/RoomModel';
import { RoomService } from '../room/room.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {
  title = 'HotelWeb';
  rooms: RoomTypeModel[] = [];
  constructor(private roomService: RoomService){}

  ngOnInit(): void {
    this.roomService.getAllRooms().subscribe((data) => {
        this.rooms = data as RoomTypeModel[];
    });
  }
}
