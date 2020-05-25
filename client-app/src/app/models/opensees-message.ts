export interface OpenseesRecivedMessage {
    ExecutionStatus : number;
    result : string;
    errorMessage: string;
    dateTime : Date;
}

export interface OpenseesExecutionMessage {
    ExecutionStatus : number;
    result : string;
    errorMessage: string;
    command: string;
    dateTime : Date;
}
