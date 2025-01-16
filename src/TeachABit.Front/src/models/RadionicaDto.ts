export interface RadionicaDto {
    id?: number;
    naziv: string;
    opis?: string;
    vlasnikId?: string;
    vlasnikUsername?: string;
    vlasnikProfilnaSlikaVersion?: string;
    brojprijavljenih?: number;
    kapacitet?: number;
    cijena?: number;
    datumvrijeme?: Date;
}
