import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AddsubtaskServiceService {

  private apiUrl = 'http://localhost:5138/Task/AddnewSubtask';

  constructor(private http: HttpClient) { }

  Addsubtask(Subtask: any): Observable<any> {
    //var temp=
    const base64String = Subtask.FileUploadTaskPdf as string; // Explicitly cast to string
    const match = base64String.match(/data:.*;base64,(.*)/);
if (match && match.length === 2) {
    const cleanedBase64String = match[1]; // Extract the base64 string
    Subtask.FileUploadTaskPdf = cleanedBase64String;
}
    //const cleanedBase64String = base64String.replace("data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,", "");
    //Subtask.FileUploadTaskPdf=cleanedBase64String;

    //reader.readAsDataURL(file);
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<any>(this.apiUrl,{Subtask}, httpOptions)
    ;
}
}
