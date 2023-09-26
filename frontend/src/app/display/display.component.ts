import {Component, OnInit} from '@angular/core';
import {ServiceComponent} from "../service/service.component";
import {Router} from "@angular/router";
import {first} from "rxjs";

@Component({
  selector: 'app-display',
  templateUrl: './display.component.html',
  styleUrls: ['./display.component.css']
})
export class DisplayComponent implements OnInit{

  person: any = null;
  parents: any[] = [];
  grandParents: any[] = [];
  siblings: any[] = [];
  kids: any[] = [];
  firstCousins: any[] = [];

  constructor(private service: ServiceComponent, private router: Router) { }

  ngOnInit() {
    const personName = localStorage.getItem('personName')!;
    this.getPerson(personName);
    this.getParents(personName);
    this.getGrandParents(personName);
    this.getSiblings(personName);
    this.getKids(personName);
    this.getFirstCousins(personName);
  }
  getPerson(name: string){
    this.service.getPerson(name).subscribe(
      (response: any) => {
        this.person = response;
      },
      (error: any) => {
        alert(error.error);
      }
    );
  }
  getParents(name: string){
    this.service.getParents(name).subscribe(
      (response: any) => {
        this.parents = response;
      },
      (error: any) => {
        alert(error.error);
      }
    );
  }
  getGrandParents(name: string){
    this.service.getGrandParents(name).subscribe(
      (response: any) => {
        this.grandParents = response;
      },
      (error: any) => {
        alert(error.error);
      }
    );
  }
  getSiblings(name: string){
    this.service.getSiblings(name).subscribe(
      (response: any) => {
        this.siblings = response;
      },
      (error: any) => {
        alert(error.error);
      }
    );
  }
  getKids(name: string){
    this.service.getKids(name).subscribe(
      (response: any) => {
        this.kids = response;
      },
      (error: any) => {
        alert(error.error);
      }
    );
  }
  getFirstCousins(name: string){
    this.service.getFirstCousins(name).subscribe(
      (response: any) => {
        this.firstCousins = response;
      },
      (error: any) => {
        alert(error.error);
      }
    );
  }

  ageCalculator(date: string) {
    // Split the date string into day, month, and year components
    const dateParts = date.split('/');

    // Ensure there are exactly 3 parts (day, month, year)
    if (dateParts.length !== 3) {
      return null; // Invalid date format
    }

    const day = parseInt(dateParts[0], 10);
    const month = parseInt(dateParts[1], 10);
    const year = parseInt(dateParts[2], 10);

    // Create a Date object from the components
    const birthDate = new Date(year, month - 1, day); // Month is 0-based

    // Get the current date
    const currentDate = new Date();

    // Calculate the age
    let age = currentDate.getFullYear() - birthDate.getFullYear();

    // Check if the birthday has occurred this year
    if (
      currentDate.getMonth() < birthDate.getMonth() ||
      (currentDate.getMonth() === birthDate.getMonth() && currentDate.getDate() < birthDate.getDate())
    ) {
      age--; // Subtract 1 year if the birthday hasn't occurred yet this year
    }

    console.log(age);
    return age;
  }


  redirectPerson(name: string){
    localStorage.setItem('personName', name);
    this.router.navigateByUrl(`/${name}`);
    window.location.reload();
  }

  redirectToMainScreen(){
    this.router.navigateByUrl("/");
  }

  protected readonly first = first;
}
