import Uloga from "../Uloga";

export interface RefreshUserInfoDto {
    id: string;
    userName: string;
    roles: Uloga[];
    isAuthenticated: boolean;
}
