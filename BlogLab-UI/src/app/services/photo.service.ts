import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BlogCreate } from '../models/blog/blog-create.model';
import { Photo } from '../models/photo/photo.model';

@Injectable({
  providedIn: 'root'
})
export class PhotoService {

  constructor(
    private http: HttpClient
  ) { }

  create(model :FormData):Observable<Photo>{
    return this.http.post<Photo>(`${environment.webapi}/photo`,model);

  }
  getByApplicationUserId():Observable<Photo[]>{
    return this.http.get<Photo[]>(`${environment.webapi}/photo`);


  }
  get(photoId:BlogCreate):Observable<Photo>{
    return this.http.get<Photo>(`${environment.webapi}/photo/${photoId}`);
  }
  delete(photoId:number):Observable<number>{
    return this.http.delete<number>(`${environment.webapi}/photo/${photoId}`);

  }

}
