// ---------------------------------------
// Email: quickapp@ebenmonney.com
// Templates: www.ebenmonney.com/templates
// (c) 2024 www.ebenmonney.com/mit-license
// ---------------------------------------

import { Component, OnInit } from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';
import { FormBuilder, ReactiveFormsModule, FormGroup, Validators } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import { fadeInOut } from '../../services/animations';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-sales-lead',
    templateUrl: './sales-lead.component.html',
  styleUrl: './sales-lead.component.scss',
    animations: [fadeInOut],
  imports: [TranslateModule, ReactiveFormsModule, CommonModule]
})
export class SalesLeadComponent implements OnInit {
  leadForm!: FormGroup;
 

  constructor(private http: HttpClient, private fb: FormBuilder) { }

  leadName: string = '';
  emailAddress: string = '';
  phoneNumber: string = '';
  productInterest: string = '';
  leadType: string = '';
  leadSource: string = '';
  leadRequirements: string = '';
  expectedBudget: string = '';
  result: boolean = false;

  ngOnInit() {
    this.leadForm = this.fb.group({
      leadName: ['', [Validators.required, Validators.minLength(3)]],
      emailAddress: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^\d{10}$/)]],
      productInterest: ['', Validators.required],
      leadType: ['', Validators.required],
      leadSource: ['', Validators.required],
      leadRequirements: ['', [Validators.required, Validators.minLength(5)]],
      expectedBudget: ['', [Validators.required, Validators.pattern(/^\d+$/)]],
    });
  }

  submitLead() {

    if (this.leadForm.invalid) {
      this.leadForm.markAllAsTouched(); // show validation errors
      return;
    }
    // Fetch the values entered in the lead form in the response object
    const request = this.leadForm.value;
    console.log(request);

    const httpOptions = {
      headers: new HttpHeaders({
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      })
    };


    return this.http.post<boolean>('https://localhost:7085/api/saleslead/create-lead', request, httpOptions)
      .pipe(
        catchError((error) => {
          console.error('Lead Create failed', error);
          return of(false); // or any fallback logic
        })
      )
      .subscribe(
        (result) => {
          this.result = result;
          console.log('Lead Created successfully:', this.result);
        }
      );
  }
}


