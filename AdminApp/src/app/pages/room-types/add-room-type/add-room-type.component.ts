import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-room-type',
  templateUrl: './add-room-type.component.html',
  styleUrls: ['./add-room-type.component.css']
})
export class AddRoomTypeComponent {
  constructor(private formBuilder: FormBuilder){}

  name = new FormControl('', [
    Validators.required,
    Validators.maxLength(20),
    Validators.minLength(3)
  ]);
  
  capacity = new FormControl('', [
    Validators.required
  ]);

  price = new FormControl('', [
    Validators.required
  ]);

  descriptionUz = new FormControl('', [
    Validators.required,
    Validators.minLength(100),
    Validators.maxLength(2000)
  ]);

  descriptionEn = new FormControl('', [
    Validators.required,
    Validators.minLength(100),
    Validators.maxLength(2000)
  ]);

  descriptionRu = new FormControl('', [
    Validators.required,
    Validators.minLength(100),
    Validators.maxLength(2000)
  ]);

  

  files = this.formBuilder.array([], [Validators.required]);

  roomTypeForm = this.formBuilder.group({
    name: this.name,
    capacity: this.capacity,
    price: this.price,
    descriptionUz: this.descriptionUz,
    descriptionEn: this.descriptionEn,
    descriptionRu: this.descriptionRu,
    files: this.files
  });

  onSubmit() {

    console.log(this.roomTypeForm.getRawValue());
  }
  
  // public onSelectedFileMultiple(event: any) {
  //   if (event.target.files.length > 0) {
  //     var array = event.target.files;
  //     this.files.push(array[0]);
  //     this.roomTypeForm.controls.files.setValue(array[0]);
  //     this.files.push(array[1]);
  //     this.roomTypeForm.controls.files.setValue(array[1])
  //     // for (let i = 0; i < event.target.files.length; i++) {
  //     //   let file = ;
  //     //   //this.files.push(file);
  //     //   (this.roomTypeForm.get('files') as FormArray).setValue(file);
  //     //   alert(file);
  //     // }
  //   }
  
}
