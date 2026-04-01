import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink, RouterLinkActive } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { NgIf, DatePipe } from '@angular/common';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-url-info',
  standalone: true,
  imports: [NgIf, DatePipe, RouterLink, RouterLinkActive],
  templateUrl: './url-info.html',
  styleUrl: './url-info.scss',
})
export class UrlInfo implements OnInit {
  url: any = null;
  loading = true;
  copied = false;

  constructor(
    private route: ActivatedRoute,
    private http: HttpClient,
    private router: Router,
    private cdr: ChangeDetectorRef,
  ) {}

  ngOnInit() {
    const code = this.route.snapshot.paramMap.get('code')!;
    this.http.get<any>(`http://localhost:5005/api/ShortUrl/${code}`).subscribe({
      next: (res) => {
        this.url = res;
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: () => {
        this.loading = false;
        this.cdr.detectChanges();
      },
    });
  }

  copy() {
    navigator.clipboard
      .writeText(`http://localhost:5005/api/ShortUrl/r/${this.url.shortCode}`)
      .then(() => {
        this.copied = true;
        this.cdr.detectChanges();
        setTimeout(() => {
          this.copied = false;
          this.cdr.detectChanges();
        }, 1800);
      });
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }
}
