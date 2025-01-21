export interface TecajDto {
    id?: number;
    naziv: string;
    opis: string;
    cijena?: number;
    kupljen?: boolean;
    vlasnikId?: string;
    vlasnikUsername?: string;
    naslovnaSlikaVersion?: string;
    vlasnikProfilnaSlikaVersion?: string;
    lekcije?: [{
        id: number,
        naziv: string,
        sadrzaj: string,
        createdDateTime?: string,
        tecajId: number,
    }];
}
