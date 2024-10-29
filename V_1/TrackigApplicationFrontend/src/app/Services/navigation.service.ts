import { Injectable } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NavigationService {

  private navigationHistory: Set<string> = new Set();

  constructor(private router: Router) {
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe((event: any) => {
      if (event instanceof NavigationEnd) {
        this.navigationHistory.add(event.urlAfterRedirects);
      }
    });
  }

  getNavigationHistory(): string[] {
    //return Array.from(this.navigationHistory).join(' / ');
    return Array.from(this.navigationHistory);
  }
}
