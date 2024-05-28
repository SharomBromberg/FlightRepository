import { FlightInterface } from "./flight-interface";

export interface JourneyInterface {
    origin: string;
    destination: string;
    price: number;
    flights: FlightInterface[];
}
