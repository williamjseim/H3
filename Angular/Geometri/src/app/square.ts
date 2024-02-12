import { Geometry } from "./geometry";

export class Square extends Geometry {
    constructor(){
        super();
        this.sides = [""];
    }

    override Omkreds() : number{
        return Number(this.sides[0]) * 4
    }

    override Area() : number{
        return Number(this.sides[0]) * Number(this.sides[0]);
    }
}
