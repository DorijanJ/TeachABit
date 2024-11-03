import {
    createContext,
    useState,
    Dispatch,
    SetStateAction,
    useContext,
    ReactNode,
} from "react";
import { AppUserDto } from "../models/AppUserDto";

interface GlobalContextProps {
    userIsLoggedIn: boolean | undefined;
    setIsUserLoggedIn: Dispatch<SetStateAction<boolean | undefined>>;
    loggedInUser: AppUserDto | undefined;
    setLoggedInUser: Dispatch<SetStateAction<AppUserDto | undefined>>;
}

const GlobalContext = createContext<GlobalContextProps>({
    loggedInUser: undefined,
    setIsUserLoggedIn: () => {},
    userIsLoggedIn: undefined,
    setLoggedInUser: () => {},
});

interface ProviderProps {
    children: ReactNode;
}

export const useGlobalContext = () => {
    return useContext(GlobalContext);
};

export function GlobalContextProvider({ children }: ProviderProps) {
    const [loggedInUser, setLoggedInUser] = useState<AppUserDto>();
    const [userIsLoggedIn, setIsUserLoggedIn] = useState<boolean>();
    return (
        <GlobalContext.Provider
            value={{
                userIsLoggedIn,
                setIsUserLoggedIn,
                loggedInUser,
                setLoggedInUser,
            }}
        >
            {children}
        </GlobalContext.Provider>
    );
}
