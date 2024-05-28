import { Component, OnInit } from '@angular/core';
import { JourneyInterface } from '../interfaces/journey-interface';
import { JourneyService } from '../services/journey.service';
import { FlightInterface } from '../interfaces/flight-interface';

@Component({
  selector: 'app-journey-list',
  templateUrl: './journey-list.component.html',
  styleUrls: ['./journey-list.component.scss']
})
export class JourneyListComponent implements OnInit {

  journeys: JourneyInterface[] = [];
  flights: FlightInterface[] = [];
  origins: string[] = [];
  destinations: string[] = [];
  currencies: string[] = ['USD', 'EUR', 'GBP', 'COP', 'ARS', 'BRL', 'MXN', 'CLP', 'PEN', 'UYU', 'VEF'];
  origin = '';
  destination = '';
  currency = '';
  allowStops = false;
  type = '';
  displayFlights: boolean = false;
  displayJourneys: boolean = false;

  constructor(private journeyService: JourneyService) { }

  ngOnInit(): void {
    this.getAllFlights();
  }

  getAllFlights(): void {
    this.journeyService.getAllFlights().subscribe(data => {

      this.flights = data;
      this.origins = [...new Set(data.map(flight => flight.origin))];
      this.destinations = [...new Set(data.map(flight => flight.destination))];

    }, error => {
      console.error('Error:', error);
    });
  }

  displayAllFlights(): void {
    this.getAllFlights();
    this.displayFlights = true;
    this.displayJourneys = false;
    console.log(this.flights);
  }

  getFlights(): void {
    this.journeyService.getFlights(this.origin, this.destination, this.currency, this.type, this.allowStops)
      .subscribe(journeys => {
        console.log(journeys);
        this.journeys = journeys;
      }, error => {
        console.error('Error:', error);
      });
  }

  displaySearchedFlights(): void {
    this.getFlights();
    this.displayJourneys = true;
    this.displayFlights = false;
  }
}