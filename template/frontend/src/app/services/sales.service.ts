import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import ApiResponse  from '../models/api-response';

var url = environment.api + '/sales';

@Injectable({
    providedIn: 'root',
})
export class SalesService {
    constructor(private http: HttpClient) { }

    createSale(data: any): Observable<ApiResponse<any>> {
        let query = `${url}/`;
        return this.http.post<ApiResponse<any>>(query, data);
    }

    getSale(data: any): Observable<ApiResponse<any>> {
        let query = `${url}/${data.id}`;
        return this.http.get<ApiResponse<any>>(query);
    }

    getList(data: any): Observable<ApiResponse<any>> {
        let query = `${url}?pageNumber=${data.pageNumber}&pageSize=${data.pageSize}`;

        if (data.saleNumber != null) {
            query += `&saleNumber=${data.saleNumber}`;
        }

        return this.http.get<ApiResponse<any>>(query);
    }


    deleteSale(data: any): Observable<ApiResponse<any>> {
        let query = `${url}/${data.id}`;
        return this.http.delete<ApiResponse<any>>(query);
    }

}