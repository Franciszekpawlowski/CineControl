import { Cinema } from "../cinema.model";
export interface GetCinemasByCityResponse {
    tenantId: string;
    city: string;
    cinemas: Cinema[];
  }