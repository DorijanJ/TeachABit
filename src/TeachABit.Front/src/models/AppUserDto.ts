import Uloga from "./Uloga";

export interface AppUserDto {
    username?: string;
    id?: string;
    profilnaSlikaVersion?: string;
    roles?: Uloga[];
    verifikacijaStatusId?: number;
    verifikacijaStatusNaziv?: string;
    korisnikStatus?: string;
    korisnikStatusId?: number;
}
