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
    pageIsLoading: boolean;
    setPageIsLoading: Dispatch<SetStateAction<boolean>>;
    isOpenAuthForm: boolean;
    setIsOpenAuthForm: Dispatch<SetStateAction<boolean>>;
}

const GlobalContext = createContext<GlobalContextProps>({
    loggedInUser: undefined,
    setIsUserLoggedIn: () => {},
    userIsLoggedIn: undefined,
    setLoggedInUser: () => {},
    pageIsLoading: false,
    setPageIsLoading: () => {},
    isOpenAuthForm: false,
    setIsOpenAuthForm: () => {},
});

interface ProviderProps {
    children: ReactNode;
}

export const useGlobalContext = () => {
    return useContext(GlobalContext);
};

export function GlobalContextProvider({ children }: ProviderProps) {
    const [loggedInUser, setLoggedInUser] = useState<AppUserDto>();
    const [pageIsLoading, setPageIsLoading] = useState(false);
    const [userIsLoggedIn, setIsUserLoggedIn] = useState<boolean>();
    const [isOpenAuthForm, setIsOpenAuthForm] = useState(false);
    return (
        <GlobalContext.Provider
            value={{
                userIsLoggedIn,
                setIsUserLoggedIn,
                loggedInUser,
                setLoggedInUser,
                pageIsLoading,
                setPageIsLoading,
                isOpenAuthForm,
                setIsOpenAuthForm,
            }}
        >
            {children}
        </GlobalContext.Provider>
    );
}
