import { Injectable } from '@angular/core';
import * as SignalR from "@aspnet/signalr";
import { OpenseesClientModel } from '../models/opensees-client-model';
import { OpenseesRecivedMessage, OpenseesExecutionMessage } from '../models/opensees-message';
import { HubInfo } from '../models/hub-info';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OpenseesHubServiceService {
  private hubConnection: SignalR.HubConnection;
  hubInfo: HubInfo;
  hubInfo$: Subject<HubInfo> = new Subject();
  recivedOpsExecMsgs: OpenseesExecutionMessage[] = [];
  connectionSuccessfullyStarted: boolean = false;
  get isConnected(): boolean {
    return false;
  }
  constructor() { }
  public createConnection = () => {
    this.hubConnection = new SignalR.HubConnectionBuilder()
      .withUrl('https://localhost:5001/ops')
      .build();

    this.hubConnection.start()
      .then(() => { this.connectionSuccessfullyStarted = true })
      .catch(err => console.log('Error while starting connection: ' + err));

    this.hubConnection.on("recived-execution-message", (message: OpenseesExecutionMessage) => {
      this.recivedOpsExecMsgs.push(message);
    });

    this.hubConnection.on("hub-info", (hubInfo: HubInfo) => {
      this.hubInfo = hubInfo;
      this.hubInfo$.next(hubInfo);
    });
  }

  public resetLogs(){
    this.recivedOpsExecMsgs = [];
  }

  getHubInfo() {
    this.hubConnection.send("get-hub-info");
  }
}
