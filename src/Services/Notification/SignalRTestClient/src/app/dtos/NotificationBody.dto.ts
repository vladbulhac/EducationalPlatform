export interface NotificationBody{
    Id:string;
    Title:string;
    From:string;
    To:string;
    Message:string;
    IssuedAt:Date;
    Uri:string;
    Seen:boolean;
}