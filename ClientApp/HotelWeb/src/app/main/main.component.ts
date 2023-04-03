import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
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
  constructor(private roomService: RoomService,
              private formBuilder: FormBuilder){}

  startDate = new FormControl('', [
    Validators.required
  ]);

  endDate = new FormControl('', [
    Validators.required
  ]);

  guestCount = new FormControl('', [
    Validators.required
  ]);

  public checkForm = this.formBuilder.group({
    startDate: this.startDate,
    endDate: this.endDate,
    guestCount: this.guestCount
  });

  ngOnInit(): void {
    this.roomService.getAllRooms().subscribe((data) => {
        this.rooms = data as RoomTypeModel[];
    });
  }

  submitCheck() {
    this.roomService.check(this.checkForm.getRawValue());
  }
}
