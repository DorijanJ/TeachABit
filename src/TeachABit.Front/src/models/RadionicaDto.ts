export interface RadionicaDto {
    id?: number;
    naziv: string;
    opis?: string;  /*provjeri treba li ovo*/
    cijena?: number;
    vlasnikId?: string;
    vlasnikUsername?: string;
    vlasnikProfilnaSlikaVersion?: string;
    brojprijavljenih?: number;      /*provjeri i ovo isto */
    kapacitet?: number;
    datumvrijeme?: Date | null;
}
