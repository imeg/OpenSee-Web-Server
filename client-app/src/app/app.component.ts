import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { OpenseesHubServiceService } from './services/opensees-hub-service.service';
import { HubConnectionInfo } from './models/hub-info';
import { of, combineLatest } from 'rxjs';
import { OpenseesService } from './services/opensees.service';
import { ExecutionCommandRequest } from './models/execution-command-request';
import { Line, Output, OutputType } from './models/output';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  command: string;
  get connectionId(): string {
    return this.openseesHubService.connInfo.connectionId;
  }
  constructor(private openseesHubService: OpenseesHubServiceService,
    private openseesService: OpenseesService) { }

  ngOnInit(): void {
    this.openseesHubService.createConnection();
  }

  execute() {
    var output = {
      id: Date.now(),
      message: "opensees >"+this.command,
      result: null,
      type: OutputType.command,
      endLine: true,
    } as Output;
    this.openseesHubService.lines.push({ outputs: [output] });
    this.openseesService.execute({ command: this.command, connectionId: this.connectionId } as ExecutionCommandRequest).subscribe(
      res => {
        console.log(res);
        this.command = "";
      }
    )
  }

  get lines(): Line[] {
    return this.openseesHubService.lines;
  }
}
