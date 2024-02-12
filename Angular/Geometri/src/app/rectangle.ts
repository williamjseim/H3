import { Square } from "./square";

export class Rectangle extends Square {

    constructor(){
        super();
        this.sides = ["", ""]
    }

    override Omkreds(): number {
        console.log(this.sides);
        return (Number(this.sides[0]) + Number(this.sides[1])) * 2
    }
    
    override Area():number{
        return Number(this.sides[0]) * Number(this.sides[1]);
    }
}
