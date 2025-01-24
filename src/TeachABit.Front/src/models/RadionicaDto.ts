export interface RadionicaDto {
    id?: number;
    naziv: string;
    opis?: string;
    cijena?: number;
    kupljena? : false;
    vlasnikId?: string;
    vlasnikUsername?: string;
    vlasnikProfilnaSlikaVersion?: string;
    brojprijavljenih?: number;
    maksimalniKapacitet?: number;
    vrijemeRadionice?: Date;
    naslovnaSlikaVersion?: string;
    favorit?: boolean;
    ocjena?: number;
}
