import { KomentarDto } from "./KomentarDto";

export interface ObjavaDto {
    id?: number;
    naziv: string;
    sadrzaj: string;
    vlasnikId?: string;
    vlasnikUsername?: string;
    vlasnikProfilnaSlikaVersion?: string;
}

export interface DetailedObjavaDto extends ObjavaDto {
    komentari?: KomentarDto[];
}
