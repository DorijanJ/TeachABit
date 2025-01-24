export interface TecajDto {
    id?: number;
    naziv: string;
    opis: string;
    cijena?: number;
    kupljen?: boolean;
    favorit?: boolean;
    vlasnikId?: string;
    vlasnikUsername?: string;
    naslovnaSlikaVersion?: string;
    vlasnikProfilnaSlikaVersion?: string;
    ocjena?: number;
    ocjenaTrenutna?: number;
    lekcije?: [
        {
            id: number;
            naziv: string;
            sadrzaj: string;
            createdDateTime?: string;
            tecajId: number;
        }
    ];
}
