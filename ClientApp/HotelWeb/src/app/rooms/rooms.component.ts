import { Component, OnInit } from '@angular/core';
import { RoomModel } from '../interfaces/RoomModel';
import { RoomService } from '../room/room.service';

@Component({
  selector: 'app-rooms',
  templateUrl: './rooms.component.html',
  styleUrls: ['./rooms.component.css']
})
export class RoomsComponent implements OnInit {
  rooms: RoomModel[] = [];

  constructor(private roomService: RoomService){
  }

  ngOnInit(): void {
    this.roomService.getAllRooms().subscribe((data) => {
        this.rooms = data as RoomModel[];
    });
  }
}
