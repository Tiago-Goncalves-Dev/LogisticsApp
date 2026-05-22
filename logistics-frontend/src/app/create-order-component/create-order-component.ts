import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import {ReactiveFormsModule} from '@angular/forms';
import { OrderService } from '../services/order';

@Component({
  selector: 'app-create-order-component',
  imports: [ReactiveFormsModule],
  templateUrl: './create-order-component.html',
  styleUrl: './create-order-component.css',
  standalone: true,
})

export class CreateOrderComponent {

  form: FormGroup;

  constructor(private fb: FormBuilder, public orderService: OrderService) {
    this.form = this.fb.group({
      // Define your form controls here
      customerId: ['default value'],
      deliveryType: ['d '],
      weightKg: ['d'],
    });
  }

  onSubmit() {
    if (this.form.valid) {
      this.orderService.postData(this.form.value).subscribe(
        response => {
          console.log('Order created successfully', response);
        },
        error => {
          console.error('Error creating order', error);
        }
      );
    }


}


}
