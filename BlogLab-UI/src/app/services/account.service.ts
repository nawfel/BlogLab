import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ApplicationUserCreate } from '../models/account/application-user-create.model';
import { ApplicationUser } from '../models/account/application-user-model';
import { map } from 'rxjs/operators'
@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private currentUserSubject$: BehaviorSubject<ApplicationUser>
  constructor(
    private http: HttpClient) {
    this.currentUserSubject$ = new BehaviorSubject<ApplicationUser>(JSON.parse(localStorage.getItem('currentUser')));
  }

  login(model: ApplicationUser): Observable<ApplicationUser> {

    return this.http.post(`${environment.webapi}/account/login`, model).pipe(map((user: ApplicationUser) => {
      if (user) {
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.setCurrentUSer(user);
      }
      return user;
    })
    )
  }

  setCurrentUSer(user: ApplicationUser) {
    this.currentUserSubject$.next(user);
  }

  register(model: ApplicationUserCreate): Observable<ApplicationUser> {
    return this.http.post(`${environment.webapi}/account/login`, model).pipe(map((user: ApplicationUser) => {
      if (user) {
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.setCurrentUSer(user);
      }
      return user;
    })
    )
  }


  setCurrentUser(user:ApplicationUser){
    this.currentUserSubject$.next(user);
  }

  public get currentUserValue() : ApplicationUser{
    return this.currentUserSubject$.value;
  }

  logout(){
    localStorage.removeItem('currentUser');
    this.currentUserSubject$.next(null);

  }
}
