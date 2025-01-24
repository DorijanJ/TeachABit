export interface RadionicaDto {
    id?: number;
    naziv: string;
    opis?: string;
    cijena?: number;
    kupljen?: false;
    vlasnikId?: string;
    vlasnikUsername?: string;
    vlasnikProfilnaSlikaVersion?: string;
    brojprijavljenih?: number;
    maksimalniKapacitet?: number;
    vrijemeRadionice?: Date;
    naslovnaSlikaVersion?: string;
    favorit?: boolean;
    ocjena?: number;
    ocjenaTrenutna?: number;
    placen?: boolean;
}
