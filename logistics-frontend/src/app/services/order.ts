import { HttpClient, httpResource } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class OrderService {

  private apiUrl = 'http://localhost:5247/api/order';

  constructor(private http: HttpClient) {}

  // Method to create\POST a new order
  postData(data: any): Observable<any> {
    return this.http.post(this.apiUrl, data);
  }
    //Method to get\GET order
    getData(): Observable<any> {
      return this.http.get(this.apiUrl);
    }
  }

