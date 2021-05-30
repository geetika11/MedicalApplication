import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '.././environments/environment';

@Injectable({
  providedIn: 'root'
})

export class ApiService {

  constructor(private http: HttpClient) { }


  LoginUser(Email: string, Password: string) {

    const userRegistrationObject = Object.assign({}, { Email, Password });
    return this.http.post(`'https:/login`, userRegistrationObject);
  }
}
