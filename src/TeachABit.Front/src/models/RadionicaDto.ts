export interface RadionicaDto {
    id?: number;
    naziv: string;
    tema?: string;
    vlasnikId?: string;
    vlasnikUsername?: string;
    vlasnikProfilnaSlikaVersion?: string;
    brojprijavljenih?: number;
    kapacitet?: number;
    datumvrijeme?: Date;
}
