import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, tap } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class AddBatchesService {

  private apiUrl = 'http://localhost:5138/Batch/AddBatch';

  constructor(private http: HttpClient) { }

  Addmentor(batch: any): Observable<any> {
    //var temp=
    const base64String = batch.Employee_info_Excel as string; // Explicitly cast to string
    const cleanedBase64String = base64String.replace("data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,", "");
    batch.Employee_info_Excel=cleanedBase64String;

    //reader.readAsDataURL(file);
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<any>(this.apiUrl,{batch}, httpOptions)
    ;
}

}


