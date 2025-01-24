import requests from "../api/agent";
import { ApiResponseDto } from "../models/common/ApiResponseDto";
import { LoginAttemptDto } from "../models/LoginAttemptDto";
import { RegisterAttemptDto } from "../models/RegistetAttemptDto";
import globalStore from "../stores/GlobalStore";

const USERNAME_KEY = "username";
const ID_KEY = "id";
const ROLES_KEY = "roles";
const STATUS_KEY = "status";

interface GoogleAuthRequest {
    token: string;
    username?: string;
}

const useAuth = () => {
    const handleUserLoggedInCheck = () => {
        const username = localStorage.getItem(USERNAME_KEY);
        const id = localStorage.getItem(ID_KEY);
        const roles = localStorage.getItem(ROLES_KEY);
        const status = localStorage.getItem(STATUS_KEY);

        if (username !== null && id !== null && roles !== null) {
            globalStore.setCurrentUser({
                username: username,
                id: id,
                roles: JSON.parse(roles),
                korisnikStatusId: !status ? undefined : parseInt(status),
            });
        } else {
            globalStore.setCurrentUser(undefined);
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
