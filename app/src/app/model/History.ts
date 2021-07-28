export class History {
    public id: number;  
    public sceneid: number;  
    public pstate: number;
    public astate: number;
    public operationHour: Date; 
    
    constructor(id: number, sceneid: number, pstate: number, astate: number, operationHour: Date){
        this.id = id;
        this.sceneid  = sceneid;
        this.pstate = pstate;
        this.astate = astate;
        this.operationHour = operationHour;           
    }
}