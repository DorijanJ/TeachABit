export interface UpdateRadionicaDto {
  id?: number;
  naziv: string;
  opis: string;
  brojprijavljenih: number     /*provjeri ovo*/;
  kapacitet?: number;
  datumvrijeme: Date | null;
}
