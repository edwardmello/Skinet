import { HttpClient } from '@angular/common/http';
import { of } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { IProduct } from './models/product';
import { IPagination } from './models/pagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'Skinet';
  products: IProduct[];
  
  constructor(private http: HttpClient){}

  ngOnInit(): void {
    //this.http.get('http://localhost:5255/api/products').subscribe((response:any) => {console.log(response)});
    this.http.get('https://localhost:7255/api/products?pageSize=50').subscribe({
      
    next : (response:IPagination) =>  this.products =response.data,
      error : (error) => console.log(error)
    });
    
  }
}
