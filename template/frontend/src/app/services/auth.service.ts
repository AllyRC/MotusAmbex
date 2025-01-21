import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import ApiResponse  from '../models/api-response';

var url = environment.api + '/Auth';

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    constructor(private http: HttpClient) { }

    login(data: any): Observable<ApiResponse<any>> {
        let query = `${url}/`;
        return this.http.post<ApiResponse<any>>(query, data);
    }

}