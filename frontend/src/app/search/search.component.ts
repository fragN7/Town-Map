import {Component, OnInit} from '@angular/core';
import {ServiceComponent} from "../service/service.component";
import {Router} from "@angular/router";

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit{

  persons: any[] = [];
  filteredPersons: any[] = [];
  searchQuery: string = '';
  selectedIndex = -1;
  ngOnInit() {
    this.getPersons();
  }
  constructor(private service: ServiceComponent, private router: Router) { }

  getPersons(){
    this.service.getAllPersons().subscribe(
      (response: any[]) => {
        this.persons = response;
      },
      (error: any) => {
        alert(error.error)
      }
    );
  }

  filterPersons() {
    this.filteredPersons = this.persons.filter((person) =>
      person.fullName.toLowerCase().includes(this.searchQuery.toLowerCase()) || person.nickName.toLowerCase().includes(this.searchQuery.toLowerCase())
    );
  }

  redirectToProfile(person: any) {
    localStorage.setItem('personName', person.fullName);
    this.router.navigateByUrl(`/${person.fullName}`);
  }

}
