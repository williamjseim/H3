import { DateTime } from "luxon";

export interface UserProgress{
    id:string;
    fakeIpFk:string;
    userIdFk:string;
    processorGhz:number;
    storageMb:number;
    memory:number;
    internetKbs:number;
    money:number;
}

export interface UserInventory{
    highTechComps:number;
    techComps:number;
    microControllers:number;
    militaryTechComps:number;
}


export enum SoftwareType{
    none,
    Firewall,
    Cracker,
    AntiVirus,
    Miner,
    Spammer,
    Virus,
}

export interface UserSoftware{
    id:string;
    type:SoftwareType;
    isInstalled:boolean;
    name:string;
    strength:number;
    size:number;
    description:string;
    isDeleteable:boolean;
    uploadId:string;
    staticObject:boolean;
}

export interface BrowserData{
    id:string;
    indexDescription:string;
    firewallStrength:number;
    crackAnswer:CrackAnswer;
}

export interface SecretKey{
    ip:string;
    verificationKey:string;
}

export interface TimedTask{
    id:string;
    endTime:string;
    softwareId:string;
}

export interface CrackAnswer{
    answer:boolean;
    username:string;
    password:string;
}

  export interface BooleanAnswer{
    boolean:boolean;
    answer:string;
}