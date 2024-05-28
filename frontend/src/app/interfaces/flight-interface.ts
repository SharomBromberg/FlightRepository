import { TransportInterface } from './transport-interface';

export interface FlightInterface {
  transport: TransportInterface;
  origin: string;
  destination: string;
  price: number;
}
