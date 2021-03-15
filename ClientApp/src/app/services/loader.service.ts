import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { LoaderComponent } from '../shared/loader/loader.component';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {
  private opened = false;
  private dialogRef: MatDialogRef<LoaderComponent>;

  constructor(private dialog: MatDialog) { }

  openDialog(): void {
    if (!this.opened) {
      this.opened = true;
      this.dialogRef = this.dialog.open(LoaderComponent, {
        data: undefined,
        maxHeight: "100%",
        width: "400px",
        maxWidth: "100%",
        disableClose: true,
        hasBackdrop: true
      });

      this.dialogRef.afterClosed().subscribe(() => {
        this.opened = false;
      });
    }
  }

  hideDialog() {
    this.dialogRef.close();
  }

}
