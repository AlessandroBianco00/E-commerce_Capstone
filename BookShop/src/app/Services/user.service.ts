import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { iUserDto } from '../Dto/user-dto';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  userUrl:string = 'https://localhost:7059/api/User'

  constructor(
    private http:HttpClient
  ) { }

  updateProfile(updatedUser:iUserDto, id:number){
    return this.http.patch<iUserDto>(`${this.userUrl}/${id}`, updatedUser)
  }
}
