import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { MainComponent } from './main/main.component';
import { NewOrderComponent } from './new-order/new-order.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { RoomComponent } from './room/room.component';
import { RoomsComponent } from './rooms/rooms.component';
import { ServerErrorComponent } from './server-error/server-error.component';

const routes: Routes = [
  {path: '', component: MainComponent},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'rooms/:roomId', component: RoomComponent},
  {path: 'rooms', component: RoomsComponent},
  {path: 'new-order/:roomId', component: NewOrderComponent},
  {path: "server-error", component: ServerErrorComponent},
  {path: '**', pathMatch: 'full', component: NotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
