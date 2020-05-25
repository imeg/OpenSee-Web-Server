import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { OpenseesHubServiceService } from './services/opensees-hub-service.service';
import { HubInfo } from './models/hub-info';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  hubInfo: HubInfo;
  constructor(private openseesHubService : OpenseesHubServiceService,
    private httpClient : HttpClient) {  }
  ngOnInit(): void {
    this.openseesHubService.createConnection();
    this.hubInfo = this.openseesHubService.hubInfo;
    this.openseesHubService.hubInfo$.subscribe(res=> this.hubInfo = res)
  }
  
  getHubInfo(): void {
    this.openseesHubService.getHubInfo();
  }

  test(): void {
    this.openseesHubService.resetLogs();
    this.httpClient.get(`https://localhost:5001/api/opensees/test/${this.hubInfo.connectionId}`).subscribe(res=> console.log(res));
  }
}
