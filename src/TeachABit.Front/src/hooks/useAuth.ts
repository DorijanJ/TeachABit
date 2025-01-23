import requests from "../api/agent";
import { useGlobalContext } from "../context/Global.context";
import { ApiResponseDto } from "../models/common/ApiResponseDto";
import { LoginAttemptDto } from "../models/LoginAttemptDto";
import { RegisterAttemptDto } from "../models/RegistetAttemptDto";

const USERNAME_KEY = "username";
const ID_KEY = "id";
const ROLES = "roles";

interface GoogleAuthRequest {
    token: string;
    username?: string;
}

const useAuth = () => {
    const globalContext = useGlobalContext();

    const handleUserLoggedInCheck = () => {
        const username = localStorage.getItem(USERNAME_KEY);
        const id = localStorage.getItem(ID_KEY);
        const roles = localStorage.getItem(ROLES);

        if (username !== null && id !== null && roles !== null) {
            globalContext.setCurrentUser({
                username: username,
                id: id,
                roles: JSON.parse(roles),
            });
            globalContext.setIsUserLoggedIn(true);
        } else {
            globalContext.setCurrentUser(undefined);
            globalContext.setIsUserLoggedIn(false);
        }
    };

    const login = async (
        loginAttempt: LoginAttemptDto
    ): Promise<ApiResponseDto | undefined> => {
        const response = await requests.postWithLoading(
            "account/login",
            loginAttempt
        );
        return response;
    };

    const loginGoogle = async (
        googleAuthRequest: GoogleAuthRequest
    ): Promise<ApiResponseDto | undefined> => {
        const response = await requests.postWithLoading(
            "account/google-signin",
            googleAuthRequest
        );
        return response;
    };

    const logout = async () => {
        await requests.postWithLoading("account/logout");
    };

    const register = async (
        registerAttempt: RegisterAttemptDto
    ): Promise<ApiResponseDto | undefined> => {
        const response = await requests.postWithLoading(
            "account/register",
            registerAttempt
        );
        return response;
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
