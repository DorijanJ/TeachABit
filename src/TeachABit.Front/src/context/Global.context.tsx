import {
    createContext,
    useState,
    Dispatch,
    SetStateAction,
    useContext,
    ReactNode,
    useMemo,
} from "react";
import { AppUserDto } from "../models/AppUserDto";

interface GlobalContextProps {
    userIsLoggedIn: boolean | undefined;
    setIsUserLoggedIn: Dispatch<SetStateAction<boolean | undefined>>;
    currentUser: AppUserDto | undefined;
    setCurrentUser: Dispatch<SetStateAction<AppUserDto | undefined>>;
    isAdmin: boolean;
}

const GlobalContext = createContext<GlobalContextProps>({
    currentUser: undefined,
    setIsUserLoggedIn: () => { },
    userIsLoggedIn: undefined,
    setCurrentUser: () => { },
    isAdmin: false,
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

    const isAdmin = useMemo(() => {
        return currentUser?.roles?.find(x => x === "Admin") !== undefined
    }, [currentUser]);

    return (
        <GlobalContext.Provider
            value={{
                userIsLoggedIn,
                setIsUserLoggedIn,
                currentUser,
                setCurrentUser,
                isAdmin,
            }}
        >
            {children}
        </GlobalContext.Provider>
    );
}
