import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { SideBarComponent } from './pages/side-bar/side-bar.component';
import { PendingOrdersComponent } from './pages/pending-orders/pending-orders.component';
import { OrdersComponent } from './pages/orders/orders.component';
import { RoomsComponent } from './pages/rooms/rooms.component';
import { AdminsComponent } from './pages/admins/admins.component';
import { LoginComponent } from './pages/auth/login/login.component';
import { RegisterComponent } from './pages/auth/register/register.component';
import { ViewComponent } from './pages/pending-orders/view/view.component';
import { RoomTypesComponent } from './pages/room-types/room-types.component';
import { HttpClientModule } from '@angular/common/http';
import { from } from 'rxjs';
import { AddRoomTypeComponent } from './pages/room-types/add-room-type/add-room-type.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    SideBarComponent,
    PendingOrdersComponent,
    OrdersComponent,
    RoomsComponent,
    AdminsComponent,
    LoginComponent,
    RegisterComponent,
    ViewComponent,
    RoomTypesComponent,
    AddRoomTypeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
