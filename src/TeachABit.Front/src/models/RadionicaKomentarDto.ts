export interface RadionicaKomentarDto {
    id?: number;
    sadrzaj: string;
    vlasnikId?: string;
    vlasnikUsername?: string;
    createdDateTime?: Date;
    lastUpdatedDateTime?: Date;
    isDeleted?: boolean;
    vlasnikProfilnaSlikaVersion?: string;
    radionicaId?: number;
    nadKomentarId?: number;
    podKomentarList?: RadionicaKomentarDto[];
    likeCount?: number | undefined;
    liked?: boolean;
}
