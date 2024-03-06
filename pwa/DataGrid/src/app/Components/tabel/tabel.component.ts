import { Component, ElementRef, Input, viewChild } from '@angular/core';
import {MatTable, MatTableModule} from '@angular/material/table';
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {MatIconModule} from '@angular/material/icon';
import {MatTooltipModule } from '@angular/material/tooltip';
import { ViewChild } from '@angular/core';
import {MatMenuModule} from '@angular/material/menu';
import { ServiceWorkerModule } from '@angular/service-worker';
import { ImageRow } from '../../Interface/image-row';
import { Observable, of } from 'rxjs';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDialog } from '@angular/material/dialog';
import { DeletionDialogComponent } from '../deletion-dialog/deletion-dialog.component';


@Component({
  selector: 'app-tabel',
  standalone: true,
  imports: [MatTable, MatTableModule, MatButtonModule, MatInputModule, MatIconModule, MatTooltipModule, MatMenuModule, ServiceWorkerModule, MatCheckboxModule],
  templateUrl: './tabel.component.html',
  styleUrl: './tabel.component.scss'
})
export class TabelComponent {

  //observable for material table
  data$:Observable<ImageRow[]> = new Observable<ImageRow[]>;
  
  //array of imagerows
  imageList:ImageRow[] = [];
  displayedColumns: string[] = ['Checked', 'Image', 'DropBox'];
  //
  @ViewChild('UploadImage') UploadButton!:ElementRef<HTMLInputElement>;
  ImageListStorage:string = "ImageList";

  constructor(public dialog:MatDialog){}

  ngOnInit(){
    
  }

  //the input button was really ugly so i made a fab button with material and call the inputbutton the this method
  UploadImageActivator(){
    this.UploadButton.nativeElement.click();
  }

  //cant rename this for some reason it throws an error
  Upload(event:Event){
    const element = event.currentTarget as HTMLInputElement;
    if(element.files?.length! == 0)
      return;

      let obj:ImageRow = {} as ImageRow;
      obj.FileType = element.files![0].type;
      obj.Size = element.files![0].size;
      this.imageList.push(obj);
      const reader = new FileReader();
      reader.onload = () =>{
        obj.ImagePath = reader.result as string;
        this.data$ = of(this.imageList);
      }
      reader.readAsDataURL(element.files![0])
      this.UpdateStorage();
  }

  //gets called then the deletion dialog box gets answered
  DeleteRow(){
    this.dialog.open(DeletionDialogComponent).afterClosed().subscribe({next:(e)=>{
      if(e == true){
        this.Delete();
      }
    }})
  }

  //deletes the marked rows
  Delete(){
    let newList:ImageRow[] = [];
    this.imageList.forEach(element => {
      if(!element.Checked)
        newList.push(element);
    });
    this.imageList = newList;
    this.data$ = of(this.imageList);
    this.UpdateStorage();
    
  }

  //updates the sessionstorage of imagerows
  UpdateStorage(){
    sessionStorage.setItem(this.ImageListStorage, JSON.stringify(this.imageList))
  }

  GetImageList(): ImageRow[]{
    if(sessionStorage.getItem(this.ImageListStorage) != null)
      return JSON.parse(sessionStorage.getItem(this.ImageListStorage)!) as ImageRow[];

    return [];
  }
}
