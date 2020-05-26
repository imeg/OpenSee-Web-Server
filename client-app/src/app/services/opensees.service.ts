import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ExecutionCommandRequest } from '../models/execution-command-request';
import { ExecutionCommandResponse } from '../models/execution-command-response';
import { Observable } from 'rxjs';

const apiUrl = "https://localhost:5001/api";
@Injectable({
  providedIn: 'root'
})
export class OpenseesService {
  constructor(private http: HttpClient) {  }

  initialize(connId: string) : Observable<any> {
    return this.http.get<any>(`${apiUrl}/opensees/initialize/${connId}`);
  }

  execute(model: ExecutionCommandRequest) : Observable<ExecutionCommandResponse> {
    return this.http.post<ExecutionCommandResponse>(`${apiUrl}/opensees/execute`, model);
  }
}
