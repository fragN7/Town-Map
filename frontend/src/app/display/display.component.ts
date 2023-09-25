import {Component, OnInit} from '@angular/core';
import {ServiceComponent} from "../service/service.component";
import {Router} from "@angular/router";

@Component({
  selector: 'app-display',
  templateUrl: './display.component.html',
  styleUrls: ['./display.component.css']
})
export class DisplayComponent implements OnInit{

  item = {
    person: null,
    parents: [],
    grandParents: [],
    siblings: [],
    kids: [],
    firstCousins: []
  }

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
        this.item.person = response;
      },
      (error: any) => {
        alert(error.error);
      }
    );
  }
  getParents(name: string){
    this.service.getParents(name).subscribe(
      (response: any) => {
        this.item.parents = response;
      },
      (error: any) => {
        alert(error.error);
      }
    );
  }
  getGrandParents(name: string){
    this.service.getGrandParents(name).subscribe(
      (response: any) => {
        this.item.grandParents = response;
      },
      (error: any) => {
        alert(error.error);
      }
    );
  }
  getSiblings(name: string){
    this.service.getSiblings(name).subscribe(
      (response: any) => {
        this.item.siblings = response;
      },
      (error: any) => {
        alert(error.error);
      }
    );
  }
  getKids(name: string){
    this.service.getKids(name).subscribe(
      (response: any) => {
        this.item.kids = response;
      },
      (error: any) => {
        alert(error.error);
      }
    );
  }
  getFirstCousins(name: string){
    this.service.getFirstCousins(name).subscribe(
      (response: any) => {
        this.item.firstCousins = response;
      },
      (error: any) => {
        alert(error.error);
      }
    );
  }
}
