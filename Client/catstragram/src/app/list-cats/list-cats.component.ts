import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Cat } from '../models/cat';
import { CatService } from '../services/cat.service';

@Component({
  selector: 'app-list-cats',
  templateUrl: './list-cats.component.html',
  styleUrls: ['./list-cats.component.css']
})
export class ListCatsComponent implements OnInit {
  cats: Cat[];
  constructor(private catService: CatService, private router: Router) { }

  ngOnInit(): void {
    this.fetchCats();
  }

  deleteCat(id) {
    this.catService.deleteCat(id)
      .subscribe(res => {
        this.fetchCats();
      });
  }

  editCat(id) {
    this.router.navigate([`cats/${id}/edit`]);
  }

  fetchCats() {
    this.catService.getCats()
      .subscribe(data => {
        this.cats = data;
      });
  }
}
