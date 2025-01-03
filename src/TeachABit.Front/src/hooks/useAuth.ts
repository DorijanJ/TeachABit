import requests from "../api/agent";
import { useGlobalContext } from "../context/Global.context";
import { AppUserDto } from "../models/AppUserDto";
import { ApiResponseDto } from "../models/common/ApiResponseDto";
import { LoginAttemptDto } from "../models/LoginAttemptDto";
import { RegisterAttemptDto } from "../models/RegistetAttemptDto";

const USERNAME_KEY = "username";
const ID_KEY = "id";
const COOKIE_TTL = "cookie_ttl";

interface GoogleAuthRequest {
    token: string;
    username?: string;
}

const useAuth = () => {
    const globalContext = useGlobalContext();

    const handleUserLoggedInCheck = () => {
        const username = localStorage.getItem(USERNAME_KEY);
        const id = localStorage.getItem(ID_KEY);
        const ttl = localStorage.getItem(COOKIE_TTL);

        if (
            username !== null &&
            id !== null &&
            ttl &&
            new Date().getTime() < parseInt(ttl)
        ) {
            globalContext.setLoggedInUser({ username: username, id: id });
            globalContext.setIsUserLoggedIn(true);
        } else {
            globalContext.setLoggedInUser(undefined);
            globalContext.setIsUserLoggedIn(false);
        }
    };

    const login = async (
        loginAttempt: LoginAttemptDto
    ): Promise<ApiResponseDto> => {
        const response = await requests.postWithLoading(
            "account/login",
            loginAttempt
        );
        const user: AppUserDto = response.data;
        if (user && user.username) {
            setAuthData(user);
        }
        return response;
    };

    const loginGoogle = async (
        googleAuthRequest: GoogleAuthRequest
    ): Promise<ApiResponseDto> => {
        const response: ApiResponseDto = await requests.postWithLoading(
            "account/google-signin",
            googleAuthRequest
        );
        const user: AppUserDto = response.data;
        if (user && user.username) {
            setAuthData(user);
        }
        return response;
    };

    const logout = async () => {
        clearAuthData();
        await requests.postWithLoading("account/logout");
    };

    const register = async (
        registerAttempt: RegisterAttemptDto
    ): Promise<ApiResponseDto> => {
        const response: ApiResponseDto = await requests.postWithLoading(
            "account/register",
            registerAttempt
        );
        return response;
    };

    const setAuthData = (appUser: AppUserDto) => {
        if (appUser.username && appUser.id) {
            window.location.reload();
            const now = new Date();
            const ttl = now.getTime() + 7 * 60 * 60 * 1000;
            localStorage.setItem(USERNAME_KEY, appUser.username);
            localStorage.setItem(ID_KEY, appUser.id);
            localStorage.setItem(COOKIE_TTL, ttl.toString());
            globalContext.setLoggedInUser({
                username: appUser.username,
                id: appUser.id,
            });
            globalContext.setIsUserLoggedIn(true);
        }
    };

    const clearAuthData = () => {
        window.location.reload();
        localStorage.removeItem(USERNAME_KEY);
        localStorage.removeItem(ID_KEY);
        globalContext.setLoggedInUser(undefined);
        globalContext.setIsUserLoggedIn(false);
    };

    const getCurrentUserName = (): string | null => {
        return localStorage.getItem(USERNAME_KEY);
    };

    const validate = (
        type: "email" | "password",
        value: string
    ): string | undefined => {
        const validators: { [key: string]: () => string | undefined } = {
            email: () => {
                const isEmailFormat = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
                return !isEmailFormat.test(value)
                    ? "Email is invalid."
                    : undefined;
            },
            password: () => {
                const hasDigit = /\d/;
                const hasLowercase = /[a-z]/;
                const hasUppercase = /[A-Z]/;
                const isValidLength = /^.{8,16}$/;

                if (!isValidLength.test(value))
                    return "Password must be between 8 and 16 characters long.";
                if (!hasDigit.test(value))
                    return "Password must contain at least one digit.";
                if (!hasLowercase.test(value))
                    return "Password must contain at least one lowercase letter.";
                if (!hasUppercase.test(value))
                    return "Password must contain at least one uppercase letter.";
            },
        };
        return validators[type]();
    };

    return {
        login,
        logout,
        register,
        loginGoogle,
        getCurrentUserName,
        handleUserLoggedInCheck,
        validateEmail: (email: string) => validate("email", email),
        validatePassword: (password: string) => validate("password", password),
    };
};

export default useAuth;
