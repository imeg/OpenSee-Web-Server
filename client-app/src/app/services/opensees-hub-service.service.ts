import { Injectable } from '@angular/core';
import * as SignalR from "@aspnet/signalr";
import { OpenseesClientModel } from '../models/opensees-client-model';
import { OpenseesRecivedMessage, OpenseesExecutionMessage } from '../models/opensees-message';
import { HubConnectionInfo } from '../models/hub-info';
import { Subject } from 'rxjs';
import { Output, Line, OutputType } from '../models/output';
import { OpenseesService } from './opensees.service';

@Injectable({
  providedIn: 'root'
})
export class OpenseesHubServiceService {
  private hubConnection: SignalR.HubConnection;
  connInfo: HubConnectionInfo;
  lines: Line[] = [];
  connInfo$: Subject<HubConnectionInfo> = new Subject();
  recivedOpsExecMsgs: OpenseesExecutionMessage[] = [];
  connectionSuccessfullyStablished: boolean = false;
  currentOutput: Output
  get isConnected(): boolean {
    return false;
  }
  constructor(private openseesService: OpenseesService) { }
  get lastLine(): Line {
    var length = this.lines.length;
    if (length == 0) return null;
    else return this.lines[length - 1];
  }

  get lastOutput(): Output {
    var lastLine = this.lastLine;
    if (lastLine == null)
      return null;
    else {
      if (lastLine.outputs == null)
        return null;
      else {
        var length = lastLine.outputs.length;
        if (length == 0)
          return null;
        else
          return lastLine.outputs[length - 1];
      }
    }
  }

  public createConnection = () => {
    this.hubConnection = new SignalR.HubConnectionBuilder()
      .withUrl('https://localhost:5001/ops')
      .build();

    this.hubConnection.start()
      .then(() => {
        this.connectionSuccessfullyStablished = true;
        this.getConnectionInfo();
      })
      .catch(err => console.log('Error while starting connection: ' + err));

    this.hubConnection.on("recived-execution-message", (message: OpenseesExecutionMessage) => {
      this.recivedOpsExecMsgs.push(message);
    });

    this.hubConnection.on("connection-info", (connInfo: HubConnectionInfo) => {
      this.connInfo = connInfo;
      this.connInfo$.next(connInfo);
      this.openseesService.initialize(connInfo.connectionId).subscribe();
    });

    this.hubConnection.on("write", (output: Output) => {
      if(output.message == "↵") return;
      output.type = OutputType.output;
      var lastLine = this.lastLine;
      if (lastLine == null) {
        lastLine = { outputs: [output], class : output.class };
        this.lines.push(lastLine);
        return;
      }

      var lastOutput = this.lastOutput;
      if (lastOutput == null) {
        this.lastLine.outputs.push(output);
        return;
      }

      if (lastOutput.endLine) {
        lastLine = { outputs: [output] , class : output.class };
        this.lines.push(lastLine);
        return;
      } else {
        this.lastLine.outputs.push(output);
      }
      console.log(output);
    });
  }

  clearScreen() {

  }

  public resetLogs() {
    this.recivedOpsExecMsgs = [];
  }

  getConnectionInfo() {
    this.hubConnection.send("get-connection-info");
  }
}
