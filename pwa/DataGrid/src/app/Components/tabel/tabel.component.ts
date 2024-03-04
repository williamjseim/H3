import { Component, ElementRef, Input, viewChild } from '@angular/core';
import {MatTable, MatTableModule} from '@angular/material/table';
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {MatIconModule} from '@angular/material/icon';
import {MatTooltipModule} from '@angular/material/tooltip';
import { ViewChild } from '@angular/core';
import {MatMenuModule} from '@angular/material/menu';

@Component({
  selector: 'app-tabel',
  standalone: true,
  imports: [MatTable, MatTableModule, MatButtonModule, MatInputModule, MatIconModule, MatTooltipModule, MatMenuModule],
  templateUrl: './tabel.component.html',
  styleUrl: './tabel.component.scss'
})
export class TabelComponent {
  data:ImageData[] = []
  displayedColumns: string[] = ['Image', 'DropBox'];
  @ViewChild('UploadImage') UploadButton!:ElementRef<HTMLInputElement>;

  ngOnInit(){

  }
  
  UploadImageActivator(){
    this.UploadButton.nativeElement.click();
  }

}
