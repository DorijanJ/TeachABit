export interface CreateOrUpdateTecajDto {
    id?: number;
    naziv: string;
    opis: string;
    cijena?: number;
    naslovnaSlikaBase64: string;
}
