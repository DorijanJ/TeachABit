export interface CreateOrUpdateRadionicaDto {
    id?: number;
    naziv: string;
    opis?: string;
    cijena?: number;
    vlasnikId?: string;
    vlasnikUsername?: string;
    vlasnikProfilnaSlikaVersion?: string;
    brojprijavljenih?: number;
    maksimalniKapacitet?: number;
    vrijemeRadionice?: Date;
    naslovnaSlikaVersion?: string;
}
