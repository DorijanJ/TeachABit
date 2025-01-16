export interface CreateOrUpdateTecajDto {
    id?: number;
    naziv: string;
    opis: string;
    cijena?: number;
    naslovnaSlika: File | null;
}
