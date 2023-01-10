import { Component, OnInit } from '@angular/core';
import { RoomType } from 'src/app/models/RoomType';
import { RoomTypeService } from './room-type.service';

@Component({
  selector: 'app-room-types',
  templateUrl: './room-types.component.html',
  styleUrls: ['./room-types.component.css']
})
export class RoomTypesComponent implements OnInit {
  public list: RoomType[] = [];

  constructor(private service: RoomTypeService){}

  ngOnInit(): void {
    this.service.getAll().subscribe(data => this.list = data);
  }
}
