import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import {
  MatButtonModule, MatCheckboxModule, 
  MatToolbarModule, MatInputModule, 
  MatProgressSpinnerModule, 
  MatCardModule,
  MatMenuModule,
  MatListModule, 
  MatIconModule, 
  MatTableModule, 
  MatPaginatorModule, 
  MatSortModule,
} from '@angular/material';


@NgModule({
  imports: [MatButtonModule, MatCheckboxModule, MatToolbarModule, MatInputModule, MatProgressSpinnerModule,
    MatCardModule, MatMenuModule, MatIconModule, MatListModule, MatTableModule, MatPaginatorModule, MatSortModule],
  exports: [MatButtonModule, MatCheckboxModule, MatToolbarModule, MatInputModule, MatProgressSpinnerModule,
    MatCardModule, MatMenuModule, MatIconModule, MatListModule, MatTableModule, MatPaginatorModule, MatSortModule]

})
export class MaterialModule { }