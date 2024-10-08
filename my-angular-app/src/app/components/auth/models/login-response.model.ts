export interface LoginResponseModel{
    message:string;
    result:{
        jwtToken:string;
        username:string;
        firstName:string;
        lastName:string;
        phoneNumber:string;
        id:string;
        address:string;
    } 
}