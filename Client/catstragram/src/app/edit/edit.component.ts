import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Cat } from '../models/cat';
import { CatService } from '../services/cat.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {
  catForm: FormGroup;
  catId: number;
  cat: Cat;
  constructor(private fb: FormBuilder, private route: ActivatedRoute, private catService: CatService, private router: Router) {
    this.catId = this.route.snapshot.params['id'];
    this.catForm = this.fb.group({
      'id': [''],
      'description': ['']
    });
  }

  ngOnInit(): void {
    this.catService.getCat(this.catId)
      .subscribe(data => {
        this.cat = data;

        this.catForm = this.fb.group({
          'id': [this.cat.id],
          'description': [this.cat.description]
        });
      });
  }

  editCat() {
    const data = this.catForm.value;
    this.catService.editCat(data)
      .subscribe(res => {
        this.router.navigate(['cats']);
      })
  }
}
