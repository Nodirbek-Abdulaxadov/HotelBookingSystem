import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { RoomModel } from '../interfaces/RoomModel';
import { RoomService } from '../room/room.service';
import { OrderService } from './order.service';

@Component({
  selector: 'app-new-order',
  templateUrl: './new-order.component.html',
  styleUrls: ['./new-order.component.css']
})
export class NewOrderComponent implements OnInit {

  roomid: number = 0;
  price: number = 0;

  startDate = new FormControl('', [
    Validators.required
  ]);
  endDate = new FormControl('', [
    Validators.required
  ]);
  numberOfAdults = new FormControl('', [
    Validators.required
  ]);

  constructor(private formBuilder: FormBuilder,
              private roomService: RoomService,
              private route: ActivatedRoute,
              private orderService: OrderService,
              private router: Router){
   }

  ngOnInit(): void {

    var logged = localStorage.getItem("data");
    if (!logged) {
        this.router.navigate(['/login']);
    }
    else {
      const routeParams = this.route.snapshot.paramMap;
      this.roomid = Number(routeParams.get('roomId'));

      this.roomService.getRoomById(this.roomid).subscribe((data) => {
        this.price = (data as RoomModel).price;
        console.log(this.price)
      });
    }
  }

  orderForm = this.formBuilder.group({
    startDate: this.startDate,
    endDate: this.endDate,
    numberOfAdults: this.numberOfAdults,
    numberOfChildren: new FormControl({value:0, disabled: false}),
    totalPrice: new FormControl({value: this.price, disabled: true}),
    additional: new FormControl(''),
    roomId: this.roomid,
    guestId: new FormControl({value: "", disabled: true})
  });

  onSubmit() {
    const el1 = document.getElementById('error')!;
    el1.style.display = 'none';
    const el = document.getElementById('waiting')!;
    el.style.display = 'block';
    this.orderService.createOrder(this.orderForm.getRawValue(), this.roomid);
  }
}
