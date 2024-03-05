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
import {MatCheckboxModule} from '@angular/material/checkbox';
import {  MatDialog,  MAT_DIALOG_DATA,  MatDialogRef,  MatDialogTitle,  MatDialogContent,  MatDialogActions,  MatDialogClose,} from '@angular/material/dialog';
import { DeletionDialogComponent } from '../deletion-dialog/deletion-dialog.component';


@Component({
  selector: 'app-tabel',
  standalone: true,
  imports: [MatTable, MatTableModule, MatButtonModule, MatInputModule, MatIconModule, MatTooltipModule, MatMenuModule, ServiceWorkerModule, MatCheckboxModule],
  templateUrl: './tabel.component.html',
  styleUrl: './tabel.component.scss'
})
export class TabelComponent {
  data$:Observable<ImageRow[]> = new Observable<ImageRow[]>;
  imageList:ImageRow[] = [];
  displayedColumns: string[] = ['Checked', 'Image', 'DropBox'];
  @ViewChild('UploadImage') UploadButton!:ElementRef<HTMLInputElement>;
  
  constructor(public dialog:MatDialog){}

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
  }

  DeleteRow(index:number){
    this.dialog.open(DeletionDialogComponent).afterClosed().subscribe({next:(e)=>{
      if(e == true){
        this.Delete();
      }
    }})
  }

  Delete(){
    let newList:ImageRow[] = [];
    this.imageList.forEach(element => {
      if(!element.Checked)
        newList.push(element);
    });
    this.imageList = newList;
    this.data$ = of(this.imageList);
    
  }
}
