import { Component, ElementRef, ViewChild } from '@angular/core';
import { NotificationBody } from './dtos/NotificationBody.dto';
import * as signalR from '@microsoft/signalr';
import { GetNotifications } from './dtos/GetNotifications.dto';
import { GetNotification } from './dtos/GetNotification.dto';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  @ViewChild('accessToken') accessToken!: ElementRef;

  public notifications: NotificationBody[];
  private connection?: signalR.HubConnection;
  public isConnectedToHub: boolean;

  constructor() {
    this.notifications = [];
    this.isConnectedToHub = false;
  }

  public ConnectToNotificationHub(): void {
    this.ConfigureHubConnection();
    this.EstablishConnectionToHub();
    this.RegisterHandlers();
  }

  private ConfigureHubConnection(): void {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl("http://localhost:16780/notifications", { accessTokenFactory: () => this.accessToken.nativeElement.value })
      .configureLogging(signalR.LogLevel.Debug)
      .build();
  }

  private EstablishConnectionToHub(): void {
    this.connection!.start()
      .then(() => { this.isConnectedToHub = true; })
      .catch(err => {
        console.log(err);
        this.isConnectedToHub = false;
      });
  }

  private RegisterHandlers(): void {
    this.connection!.on("ReceiveNotification", (newNotification) => {
      console.log(newNotification);

      this.notifications.unshift(newNotification);
    });

    this.connection!.on("ReceiveNotifications", (newNotifications) => {
      console.log(newNotifications);

      this.ClearNotifications();
      for (let newNotification of newNotifications)
        this.notifications.push(newNotification);

      console.log("notifications now:" + this.notifications);
    });
  }

  public GetNotifications(): void {
    let dto: GetNotifications = {
      Offset: 0,
      ResultsCount: 10
    };

    this.connection!.send("GetNotifications", dto);
  }

  public NotificationSeen(idElelement: HTMLElement): void {
    let dto: GetNotification = {
      EventId: idElelement.innerText
    };

    this.connection!.send("Seen", dto);
  }

  public ClearNotifications(): void {
    this.notifications.splice(0, this.notifications.length);
  }
}
