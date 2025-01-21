import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import ApiResponse  from '../models/api-response';

var url = environment.api + '/users';

@Injectable({
    providedIn: 'root',
})
export class UserService {
    constructor(private http: HttpClient) { }

    createUser(data: any): Observable<ApiResponse<any>> {
        let query = `${url}/`;
        return this.http.post<ApiResponse<any>>(query, data);
    }

    getUser(data: any): Observable<ApiResponse<any>> {
        let query = `${url}/${data.id}`;
        return this.http.get<ApiResponse<any>>(query);
    }

    deleteUser(data: any): Observable<ApiResponse<any>> {
        let query = `${url}/${data.id}`;
        return this.http.delete<ApiResponse<any>>(query);
    }

}