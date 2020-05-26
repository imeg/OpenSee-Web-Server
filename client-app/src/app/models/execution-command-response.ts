export interface ExecutionCommandResponse {
    ExecutionStatus : number;
    result : string;
    errorMessage: string;
    command: string;
    dateTime : Date;
    duration: number;
}