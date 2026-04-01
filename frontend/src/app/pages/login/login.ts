import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgIf } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, NgIf],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login {
  login = '';
  password = '';
  loading = false;
  error = '';

  constructor(
    private http: HttpClient,
    private router: Router,
  ) {}

  onSubmit() {
    if (this.loading) return;
    this.loading = true;
    this.error = '';

    this.http
      .post<any>('http://localhost:5005/api/Auth/login', {
        login: this.login,
        password: this.password,
      })
      .subscribe({
        next: (res) => {
          localStorage.setItem('token', res.token);
          this.loading = false;
          this.router.navigateByUrl('/urls');
        },
        error: () => {
          this.error = 'Invalid login or password.';
          this.loading = false;
        },
      });
  }
}
