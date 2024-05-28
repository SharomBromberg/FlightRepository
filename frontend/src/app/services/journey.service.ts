import { Injectable } from '@angular/core';
import { environment } from '../environment/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FlightInterface } from '../interfaces/flight-interface';
import { JourneyInterface } from '../interfaces/journey-interface';

@Injectable({
  providedIn: 'root',
})
export class JourneyService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getAllFlights(): Observable<FlightInterface[]> {
    return this.http.get<FlightInterface[]>(`${this.apiUrl}/Flight/AllFlights`);
  }

  getOneWayFlights(origin: string, destination: string, currency: string, type: string, allowStops: boolean): Observable<JourneyInterface> {
    return this.http.get<JourneyInterface>(`${this.apiUrl}/Flight/Flights?origin=${origin}&destination=${destination}&currency=${currency}&type=oneway&allowStops=${allowStops}`);
  }

  getRoundTripFlights(origin: string, destination: string, currency: string, type: string, allowStops: boolean): Observable<JourneyInterface> {
    return this.http.get<JourneyInterface>(`${this.apiUrl}/Flight/Flights?origin=${origin}&destination=${destination}&currency=${currency}&type=round&allowStops=${allowStops}`);
  }                                                      //Flight/Flights?origin=MZL&destination=BOG&currency=USD&type=oneway&allowStops=true

  getFlights(origin: string, destination: string, currency: string, type: string, allowStops: boolean): Observable<JourneyInterface[]> {
    return this.http.get<JourneyInterface[]>(`${this.apiUrl}/Flight/Flights?origin=${origin}&destination=${destination}&currency=${currency}type=${type}&allowStops=${allowStops}`);
  }
}
