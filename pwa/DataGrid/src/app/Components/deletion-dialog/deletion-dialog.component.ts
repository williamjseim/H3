import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogRef } from '@angular/material/dialog';
import { MatIcon } from '@angular/material/icon';
import { MatDialogClose } from '@angular/material/dialog';

@Component({
  selector: 'app-deletion-dialog',
  standalone: true,
  imports: [MatIcon, MatButtonModule, MatDialogClose],
  templateUrl: './deletion-dialog.component.html',
  styleUrl: './deletion-dialog.component.scss'
})
export class DeletionDialogComponent {
  constructor(public dialogRef: MatDialogRef<DeletionDialogComponent>){

  }

  Yes(){
    this.CloseDialog(true);
  }

  No(){
    this.CloseDialog(false);
  }

  CloseDialog(answer:boolean){
    this.dialogRef.close(answer);
  }
}
