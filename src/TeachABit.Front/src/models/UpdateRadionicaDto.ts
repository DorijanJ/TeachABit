export interface UpdateRadionicaDto {
  id?: number;
  naziv: string;
  opis?: string;
  cijena: number;
  maksimalniKapacitet?: number;
  vrijemeRadionice?: Date;
  naslovnaSlikaVersion?: string;
}
