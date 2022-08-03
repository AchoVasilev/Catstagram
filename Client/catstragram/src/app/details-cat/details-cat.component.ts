import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Cat } from '../models/cat';
import { CatService } from '../services/cat.service';

@Component({
  selector: 'app-details-cat',
  templateUrl: './details-cat.component.html',
  styleUrls: ['./details-cat.component.css']
})
export class DetailsCatComponent implements OnInit {
  id: string;
  cat: Cat
  constructor(private route: ActivatedRoute, private catService: CatService) {
    this.id = this.route.snapshot.params['id'];
  }

  ngOnInit(): void {
    this.catService.getCat(this.id)
      .subscribe(data => {
        this.cat = data;
      });
  }

}
