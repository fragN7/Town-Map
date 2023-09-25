import { Component } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";

@Component({
  selector: 'app-service',
  templateUrl: './service.component.html',
  styleUrls: ['./service.component.css']
})
export class ServiceComponent {

  private URL = 'https://localhost:7185/api/person';
  constructor(private http: HttpClient, private router: Router) {}
  getPerson(name: string){
    return this.http.get<any>(`${this.URL}/${name}`);
  }
  getAllPersons(){
    return this.http.get<any[]>(`${this.URL}s`);
  }
  getParents(name: string){
    return this.http.get<any[]>(`${this.URL}/${name}/parents`);
  }
  getGrandParents(name: string){
    return this.http.get<any[]>(`${this.URL}/${name}/grandparents`);
  }
  getSiblings(name: string){
    return this.http.get<any[]>(`${this.URL}/${name}/siblings`);
  }
  getKids(name: string){
    return this.http.get<any[]>(`${this.URL}/${name}/kids`);
  }
  getFirstCousins(name: string){
    return this.http.get<any[]>(`${this.URL}/${name}/cousins/first`);
  }
}
