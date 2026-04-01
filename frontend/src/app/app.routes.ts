import { Routes } from '@angular/router';
import { Login } from './pages/login/login';
import { Urls } from './pages/urls/urls';
import { UrlInfo } from './pages/url-info/url-info';
import { authGuard } from './guards/auth-guard';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: Login },
  { path: 'urls', component: Urls },
  { path: 'urls/:code', component: UrlInfo, canActivate: [authGuard] },
  { path: '**', redirectTo: 'urls' },
];
