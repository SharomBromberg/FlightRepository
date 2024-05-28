import { FlightInterface } from "./flight-interface";

export interface JourneyInterface {
    flights: FlightInterface[];
    origin: string;
    destination: string;
    price: number;
}