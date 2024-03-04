import { Component, ElementRef, Input, viewChild } from '@angular/core';
import {MatTable, MatTableModule} from '@angular/material/table';
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {MatIconModule} from '@angular/material/icon';
import {MatTooltipModule} from '@angular/material/tooltip';
import { ViewChild } from '@angular/core';
import {MatMenuModule} from '@angular/material/menu';
import { ServiceWorkerModule } from '@angular/service-worker';
import { ImageRow } from '../../Interface/image-row';
import { Observable, merge, of } from 'rxjs';


@Component({
  selector: 'app-tabel',
  standalone: true,
  imports: [MatTable, MatTableModule, MatButtonModule, MatInputModule, MatIconModule, MatTooltipModule, MatMenuModule, ServiceWorkerModule],
  templateUrl: './tabel.component.html',
  styleUrl: './tabel.component.scss'
})
export class TabelComponent {
  data$:Observable<ImageRow[]> = new Observable<ImageRow[]>;
  imageList:ImageRow[] = [];
  displayedColumns: string[] = ['Image', 'DropBox'];
  @ViewChild('UploadImage') UploadButton!:ElementRef<HTMLInputElement>;
  
  constructor(){}

  ngOnInit(){
    
  }

  ngAfterViewInit(){
    //this.UploadButton.nativeElement.addEventListener("change", this.UploadImage)
  }

  Test(){

  }
  
  UploadImageActivator(){
    this.UploadButton.nativeElement.click();
  }

  UploadImage(images:File){
    console.log(this.UploadButton.nativeElement.id)
    if(this.UploadButton.nativeElement.files!.length >= 1){
      console.log(this.UploadButton.nativeElement.files?.item(0)?.type);
    }
  }

  UploadTest(event:Event){
    const element = event.currentTarget as HTMLInputElement;
    if(element.files?.length! > 0){
      let obj:ImageRow = {} as ImageRow;
      obj.Image = element.files![0];
      obj.FileType = element.files![0].type;
      obj.Size = 50;
      this.imageList.push(obj);
      this.data$ = of(this.imageList);
    }
  }

}
