import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminsComponent } from './pages/admins/admins.component';
import { LoginComponent } from './pages/auth/login/login.component';
import { RegisterComponent } from './pages/auth/register/register.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { OrdersComponent } from './pages/orders/orders.component';
import { PendingOrdersComponent } from './pages/pending-orders/pending-orders.component';
import { ViewComponent } from './pages/pending-orders/view/view.component';
import { RoomsComponent } from './pages/rooms/rooms.component';

const routes: Routes = [
  {path: '', component: DashboardComponent},
  {path: 'dashboard', component: DashboardComponent},
  {path: 'pending-orders', component: PendingOrdersComponent,
          children: [{path: 'view/:orderId', component: ViewComponent}]},
  {path: 'orders', component: OrdersComponent},
  {path: 'rooms', component: RoomsComponent},
  {path: 'admins', component: AdminsComponent},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
