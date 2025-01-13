import Uloga from "./Uloga";

export interface AppUserDto {
    username?: string;
    id?: string;
    profilnaSlikaVersion?: string;
    roles?: Uloga[];
    verificiran?: boolean;
}
