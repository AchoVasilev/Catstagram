import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CatService } from '../services/cat.service';

@Component({
  selector: 'app-createpost',
  templateUrl: './createpost.component.html',
  styleUrls: ['./createpost.component.css']
})
export class CreatepostComponent {
  postForm: FormGroup;
  constructor(private fb: FormBuilder, private catService: CatService) {
    this.postForm = this.fb.group({
      'imageUrl': ['', Validators.required],
      'description': ['']
    })
  }

  create() {
    this.catService.create(this.postForm.value)
      .subscribe(data => {
        console.log(data);
      })
  }

  get imageUrl() {
    return this.postForm.value('imageUrl');
  }
}
