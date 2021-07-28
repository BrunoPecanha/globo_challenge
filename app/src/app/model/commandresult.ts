import { Scene } from "./Scene";

export class CommandResult {
    valid: Boolean;
    error: Boolean;
    message: string;
    log: any;
    
    constructor(valid: Boolean, error: Boolean, message: string, Log: Array<Scene>){
        this.valid = valid;
        this.error = error;
        this.message = message;
        this.log = Log;          
    }
}