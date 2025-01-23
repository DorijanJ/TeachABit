export interface RadionicaDto {
    id?: number;
    naziv: string;
    opis?: string /*provjeri treba li ovo*/;
    cijena?: number;
    vlasnikId?: string;
    vlasnikUsername?: string;
    vlasnikProfilnaSlikaVersion?: string /*provjeri treba li i ovo*/;
    brojprijavljenih?: number /*provjeri i ovo isto */;
    maksimalniKapacitet?: number;
    vrijemeRadionice?: Date;
    naslovnaSlikaVersion?: string;
}
