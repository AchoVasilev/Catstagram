import { Component, OnInit } from '@angular/core';
import { Cat } from '../models/cat';
import { CatService } from '../services/cat.service';

@Component({
  selector: 'app-list-cats',
  templateUrl: './list-cats.component.html',
  styleUrls: ['./list-cats.component.css']
})
export class ListCatsComponent implements OnInit {
  cats: Cat[];
  constructor(private catService: CatService) { }

  ngOnInit(): void {
    this.catService.getCats()
      .subscribe(data => {
        this.cats = data;
      })
  }

}
