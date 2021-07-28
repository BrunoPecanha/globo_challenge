import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Scene } from '../model/Scene';
import { CommandResult } from '../model/commandresult';

@Injectable({
  providedIn: 'root'
})
export class SceneService {
  constructor(private api: HttpClient) { }

  protected url(parametros: string): string {
    return `${environment.api_url}/${parametros}`;
  }

  async getScenes(
    pagina: number,
    quantidade: number
  ): Promise<CommandResult> {
    try {      
      const url = this.url(`?page=${pagina}&qtt=${quantidade}`);
      return await this.api.get<CommandResult>(url)
      .toPromise()
      .then(
        x => x.log
      );      
    } catch (error) {
      console.error(error);
      throw error;
    }
  }

  async getHistory(id: number): Promise<{ data: Array<History>}> {
    try {
      const url = this.url(`?history=${id}`);
      return await this.api.get<{ data: Array<History>, total: number }>(url)
      .pipe(map(x => {
        return {
          data: x.data
        };
      })).toPromise();

    } catch (error) {
      console.error(error);
      throw error;
    }
  }

  async create(name: string): Promise<any> {
    try {
      await this.api.post<Scene>(this.url(""), {
        name: name
      }).toPromise();
    } catch (error) {
        const httpError: HttpErrorResponse = error;
        if (Array.isArray(httpError.error)) {
            throw error
        } else {
          throw error
        }
    }
  }

  async update(
    id: number,
    nextState: number, 
    operationHour : Date
  ): Promise<any> {
    try {
      await this.api.put<Scene>(this.url(""), {
        id: id,
        nextState: nextState,
        operationHour: operationHour       
      }).toPromise();
    } catch (error) {
      const httpError: HttpErrorResponse = error;
      if (Array.isArray(httpError.error)) {
        throw error;
      } else {
        throw error;
      }
    }
  }  
}

