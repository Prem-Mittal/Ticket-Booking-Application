import { Event } from "../../pages/models/Event.model";
import { User } from "./user.model";

export interface BookingModel{
    id:string;
    noOfTickets:number;
    amount:number;
    name:string;
    phoneNumber:string;
    email:string;
    bookingTime:string;
    event:Event;
    users:User;
}