import { NgModule } from '@angular/core';
import {RouterModule, Routes} from "@angular/router";
import {SearchComponent} from "./search/search.component";
import {DisplayComponent} from "./display/display.component";

const routes : Routes = [
  {path: '', component: SearchComponent},
  {path: ':fullname', component: DisplayComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule {}
