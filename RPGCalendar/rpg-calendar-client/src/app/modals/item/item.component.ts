import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Item}  from 'src/app/models/item';
import { ItemService } from 'src/app/services/item/item.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';



@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.css']
})
export class ItemComponent implements OnInit {
  @ViewChild('addItemForm', {static: false}) addItemForm: NgForm;

  constructor(private itemService: ItemService, public dialogRef: MatDialogRef<ItemComponent>, @Inject(MAT_DIALOG_DATA) public data: number) { }

  ngOnInit(): void {
  }

  addItem(){
    if(this.addItemForm.valid){
      this.itemService.AddItem({
        name: this.addItemForm.value.itemName,
        description: this.addItemForm.value.itemText,
        quantity: this.addItemForm.value.itemQuanity,
        quality: this.addItemForm.value.itemQuality,
        userId: this.data
      } as Item).subscribe(() => (this.dialogRef.close()));
    }
  }

}
