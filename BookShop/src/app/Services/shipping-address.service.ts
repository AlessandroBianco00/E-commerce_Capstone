import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { iShippingAddress } from '../Models/shipping-address';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ShippingAddressService {

  addressUrl:string = 'https://localhost:7059/api/ShippingAddress'

  myAddressesSubject = new BehaviorSubject<iShippingAddress[]>([])
  myAddresses$ = this.myAddressesSubject.asObservable()

  constructor(
    private http:HttpClient
  ) {
    this.getMyAddresses().subscribe((addresses) => this.myAddressesSubject.next(addresses))
  }

  //CRUD riservate a indirizzi dell'utente attuale

  getMyAddresses(){
    return this.http.get<iShippingAddress[]>(this.addressUrl)
  }

  getAddressById(id:number){
    return this.http.get<iShippingAddress>(`${this.addressUrl}/${id}`)
  }

  createNewAddress(newAddress:Partial<iShippingAddress>) {
    return this.http.post<iShippingAddress>(this.addressUrl, newAddress)
  }

  updateAddress(updatedAddress:iShippingAddress, id:number){
    return this.http.put(`${this.addressUrl}/${id}`, updatedAddress)
  }

  deleteAddress(id:number){
    return this.http.delete(`${this.addressUrl}/${id}`)
  }
}
