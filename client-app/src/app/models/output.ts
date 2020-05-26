export interface Line {
    outputs: Output[];
    class?: string;
}
export enum OutputType {
    command = 1,
    output = 2,
}
export interface Output {
    id : number;
    message: string;
    result : string;
    class?: string;
    endLine: boolean;
    type: OutputType;
}
