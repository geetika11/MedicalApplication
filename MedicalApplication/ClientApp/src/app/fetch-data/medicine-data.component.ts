
import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DatePipe } from '@angular/common';
@Component({
  selector: 'app-fetch-data',
  templateUrl: './medicine-data.component.html',
  styleUrls: ['./medicine-data-component.css']
})

export class MedicineDataComponent {
  public medicineData: MedicineDetails[];

  newRecord: MedicineDetails = { name: '', brand: "", price: 0, quantity: 0, expiryDate: new Date(), quantityBGColor: '#ffffff', expiryDateBGColor: '#ffffff', notes:'' };
  isAddClicked = false;
  searchText: string;
  medicineName: string;
  medicineBrand: string;
  medicinePrice: number;
  medicineQuantity: number;
  medicineExpiry: Date;
  errorMessage: string;
  saveSuccessful: false;
  medicineNotes: string;
  todayDate : string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private datePipe:DatePipe) {

    http.get<any>(baseUrl + 'medicine/getMedicineList').subscribe(result => {

      if (result.isSuccess) {
        this.medicineData = result.data;
      }
      else {
        this.errorMessage = result.message;
      }
    }, error => console.error(error));
  }


  save() {
    this.newRecord.name = this.medicineName;
    this.newRecord.brand = this.medicineBrand;
    this.newRecord.price = this.medicinePrice;
    this.newRecord.quantity = this.medicineQuantity;
    this.newRecord.expiryDate = this.medicineExpiry;
    this.newRecord.notes = this.medicineNotes;
    this.http.post<any>('https://localhost:44368/' + 'medicine/' + 'saveMedicine', this.newRecord).subscribe(data => {
      console.log(data);
      if (data.isSuccess) {
        window.location.reload();
      }

      else {
        this.saveSuccessful = data.isSuccess;
        this.errorMessage = data.message;
      }
    })
  }

  cancel() {
    this.isAddClicked = false;
  }

  addRow() {
    this.todayDate = this.datePipe.transform(new Date(), 'yyyy-MM-dd');
    
    this.isAddClicked = true;
  }
}

interface MedicineDetails {
  name: string;
  brand: string;
  price: number;
  quantity: number;
  expiryDate: Date;
  quantityBGColor: string;
  expiryDateBGColor: string;
  notes: string;
}
