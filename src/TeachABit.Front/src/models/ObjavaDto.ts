import { KomentarDto } from "./KomentarDto";

export interface ObjavaDto {
    id?: number;
    naziv: string;
    sadrzaj: string;
    vlasnikId?: number;
    vlasnikUsername?: string;
}

export interface DetailedObjavaDto extends ObjavaDto {
    komentari?: KomentarDto[];
}
