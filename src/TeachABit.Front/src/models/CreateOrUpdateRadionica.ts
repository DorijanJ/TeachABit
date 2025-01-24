export interface CreateOrUpdateRadionicaDto {
  id?: number;
  naziv: string;
  opis?: string;
  cijena: number;
  maksimalniKapacitet?: number;
  vrijemeRadionice?: Date;
  naslovnaSlikaBase64?: string;
}
