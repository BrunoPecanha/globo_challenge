export class Scene {
    public id: number;  
    public state: string;
    public name: string;
    public operationHour: Date; 
    
    constructor(id: number, state: number, name: string, operationHour: Date){
        this.id = id;
        this.name = name;
        this.state = state == 1 ? "Pendente" : state == 2 ? "Preparada" : state == 3 ? "Gravada" : "Pendurada";
        this.operationHour = operationHour;       
    }
}