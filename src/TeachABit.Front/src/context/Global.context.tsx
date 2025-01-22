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

interface GlobalContextProps {
    userIsLoggedIn: boolean | undefined;
    setIsUserLoggedIn: Dispatch<SetStateAction<boolean | undefined>>;
    currentUser: AppUserDto | undefined;
    setCurrentUser: Dispatch<SetStateAction<AppUserDto | undefined>>;
    hasPermissions: (level: LevelPristupa) => boolean;
}

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
            return (
                currentUser?.roles?.find((x) => x.levelPristupa >= level) !==
                undefined
            );
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
