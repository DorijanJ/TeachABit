import {
    createContext,
    useState,
    Dispatch,
    SetStateAction,
    useContext,
    ReactNode,
    useCallback,
} from "react";
import { AppUserDto } from "../models/AppUserDto";
import { LevelPristupa } from "../enums/LevelPristupa";
import Uloga from "../models/Uloga";

interface GlobalContextProps {
    userIsLoggedIn: boolean | undefined;
    setIsUserLoggedIn: Dispatch<SetStateAction<boolean | undefined>>;
    currentUser: AppUserDto | undefined;
    setCurrentUser: Dispatch<SetStateAction<AppUserDto | undefined>>;
    hasPermissions: (level: LevelPristupa) => boolean;
}

const ROLES = "roles";

const GlobalContext = createContext<GlobalContextProps>({
    currentUser: undefined,
    setIsUserLoggedIn: () => {},
    userIsLoggedIn: undefined,
    setCurrentUser: () => {},
    hasPermissions: () => false,
});

interface ProviderProps {
    children: ReactNode;
}

export const useGlobalContext = () => {
    return useContext(GlobalContext);
};

export function GlobalContextProvider({ children }: ProviderProps) {
    const [currentUser, setCurrentUser] = useState<AppUserDto>();
    const [userIsLoggedIn, setIsUserLoggedIn] = useState<boolean>();

    const hasPermissions = useCallback(
        (level: LevelPristupa) => {
            var has =
                currentUser?.roles?.find((x) => x.levelPristupa >= level) !==
                undefined;
            if (!has) {
                const updatedRoles = localStorage.getItem(ROLES);
                if (updatedRoles == null) return false;
                const r: Uloga[] = JSON.parse(updatedRoles);
                if (r.find((x) => x.levelPristupa >= level) !== undefined)
                    has = true;
            }
            return has;
        },
        [currentUser]
    );

    return (
        <GlobalContext.Provider
            value={{
                userIsLoggedIn,
                setIsUserLoggedIn,
                currentUser,
                setCurrentUser,
                hasPermissions,
            }}
        >
            {children}
        </GlobalContext.Provider>
    );
}
