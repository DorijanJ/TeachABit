export interface KomentarDto {
    id?: number;
    sadrzaj: string;
    vlasnikId?: string;
    vlasnikUsername?: string;
    createdDateTime?: Date;
    lastUpdatedDateTime?: Date;
    isDeleted?: boolean;
    vlasnikProfilnaSlikaVersion?: string;
    objavaId?: number;
    nadKomentarId?: number;
    podKomentarList?: KomentarDto[];
    likeCount?: number | undefined;
    liked?: boolean;
}
