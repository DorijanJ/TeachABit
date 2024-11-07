export interface KomentarDto {
    id?: number;
    sadrzaj: string;
    vlasnikId: number;
    vlasnikUsername?: string;
    createdDateTime: Date;
}
