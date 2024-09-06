import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { filter } from 'rxjs';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrl: './search-bar.component.scss'
})
export class SearchBarComponent   {

  searchForm!: FormGroup;

  constructor(private router: Router) {}

  ngOnInit() {
    this.searchForm = new FormGroup({
      filter: new FormControl('category'), // default value
      search: new FormControl('')
    });
  }

  searchParameter() {
    const filter = this.searchForm.get('filter')?.value;
    const search = this.searchForm.get('search')?.value;
    // Navigazione intermedia necessaria perchè funzioni il navigate sullo stesso indirizzo
    this.router.navigateByUrl('/profile', { skipLocationChange: true }).then(() => {
      this.router.navigate(['/search'], { queryParams: { [filter]: search } });
    });
  }
}
