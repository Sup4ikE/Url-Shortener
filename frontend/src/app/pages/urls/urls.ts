import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NgFor, NgIf, DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-urls',
  standalone: true,
  imports: [NgFor, NgIf, FormsModule, DatePipe, RouterModule],
  templateUrl: './urls.html',
  styleUrl: './urls.scss',
})
export class Urls implements OnInit {
  urls: any[] = [];
  newUrl = '';
  isAdding = false;
  addError = '';
  deletingCodes: string[] = [];
  copied = '';
  loading = true;

  isLoggedIn = false;
  isAdmin = false;
  currentUserId = 0;

  private readonly API = 'http://localhost:5005/api/ShortUrl';

  constructor(
    private http: HttpClient,
    private router: Router,
    private cdr: ChangeDetectorRef,
  ) {}

  ngOnInit(): void {
    const token = localStorage.getItem('token');
    this.isLoggedIn = !!token;

    if (token) {
      try {
        const payload = JSON.parse(atob(token.split('.')[1]));
        this.isAdmin =
          payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] === 'Admin' ||
          payload['role'] === 'Admin';

        this.currentUserId = +payload['userId'];
      } catch {}
    }

    this.loadUrls();
  }

  canDelete(url: any): boolean {
    return this.isAdmin || url.createdById === this.currentUserId;
  }

  loadUrls() {
    this.loading = true;
    this.http.get<any[]>(this.API).subscribe({
      next: (res) => {
        this.urls = res;
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: () => {
        this.loading = false;
        this.cdr.detectChanges();
      },
    });
  }

  add() {
    if (this.isAdding) return;
    this.addError = '';

    if (!this.newUrl.match(/^https?:\/\/.+/)) {
      this.addError = 'URL must start with http:// or https://';
      return;
    }

    this.isAdding = true;

    this.http.post<any>(this.API, { Url: this.newUrl }).subscribe({
      next: (res) => {
        this.urls.unshift({
          originalUrl: this.newUrl,
          shortCode: res?.shortCode ?? '???',
          createdDate: new Date().toISOString(),
        });
        this.newUrl = '';
        this.isAdding = false;
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.addError =
          typeof err.error === 'string'
            ? err.error
            : (err.error?.message ?? 'Failed to shorten URL.');
        this.isAdding = false;
        this.cdr.detectChanges();
      },
    });
  }

  delete(code: string) {
    this.deletingCodes.push(code);
    this.http.delete(`${this.API}/${code}`).subscribe({
      next: () => {
        this.urls = this.urls.filter((u) => u.shortCode !== code);
        this.deletingCodes = this.deletingCodes.filter((c) => c !== code);
        this.cdr.detectChanges();
      },
      error: () => {
        this.deletingCodes = this.deletingCodes.filter((c) => c !== code);
        this.cdr.detectChanges();
      },
    });
  }

  copy(code: string) {
    const url = `http://localhost:5005/api/ShortUrl/r/${code}`;
    navigator.clipboard.writeText(url).then(() => {
      this.copied = code;
      this.cdr.detectChanges();
      setTimeout(() => {
        this.copied = '';
        this.cdr.detectChanges();
      }, 1500);
    });
  }

  isDeleting(code: string): boolean {
    return this.deletingCodes.includes(code);
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }
}
