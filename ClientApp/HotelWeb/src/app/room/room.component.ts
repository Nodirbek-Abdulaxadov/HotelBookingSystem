import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RoomTypeModel } from '../interfaces/RoomModel';
import { RoomService } from './room.service';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.css']
})


export class RoomComponent implements OnInit{
  constructor(private route: ActivatedRoute,
              private roomService: RoomService) { }

  public room: RoomTypeModel | undefined;

  ngOnInit(): void {
    const routeParams = this.route.snapshot.paramMap;
    const roomIdFromRoute = Number(routeParams.get('roomId'));

    this.roomService.getRoomById(roomIdFromRoute).subscribe((data) => {
      this.room = data as RoomTypeModel;
    });
  }
  
}