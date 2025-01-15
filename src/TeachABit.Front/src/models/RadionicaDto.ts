export interface RadionicaDto {
  id?: number;
  naziv: string;
  tema: string;  /*provjeri treba li ovo*/ 
  predavacId?: string;
  predavac?: string;
  predavacProfilnaSlika?: string; /*provjeri treba li i ovo*/ 
  brojprijavljenih?: number;      /*provjeri i ovo isto */
  kapacitet?: number;
  datumvrijeme?: Date;
}
