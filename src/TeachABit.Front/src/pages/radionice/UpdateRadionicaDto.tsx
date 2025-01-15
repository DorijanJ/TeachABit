export interface UpdateRadionicaDto {
  id?: number;
  naziv: string;
  tema: string;
  brojprijavljenih: number     /*provjeri ovo*/;
  kapacitet?: number;
  datumvrijeme: Date;
}
