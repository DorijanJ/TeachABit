export interface KomentarDto {
    id?: number;
    sadrzaj: string;
    vlasnikId?: string;
    vlasnikUsername?: string;
    createdDateTime?: Date;
    vlasnikProfilnaSlikaVersion?: string;
    objavaId?: number;
    nadKomentarId?: number;
    podKomentarList?: KomentarDto[];
    likeCount?: number | undefined;
    liked?: boolean;
}
