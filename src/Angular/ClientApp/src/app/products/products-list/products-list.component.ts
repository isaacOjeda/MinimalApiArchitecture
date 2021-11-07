import { Component, OnInit } from '@angular/core';
import { Client, GetProducts_Response } from '../../../services/api-client';

@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.css']
})
export class ProductsListComponent implements OnInit {

  public products: GetProducts_Response[] = [];

  constructor(private client: Client) { }

  ngOnInit(): void {
    this.client.getProducts()
      .subscribe(products => this.products = products);
  }

}
