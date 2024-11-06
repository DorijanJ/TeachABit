import {
    TextField,
    Button,
    Alert,
    Link,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
} from "@mui/material";
import { useState, FormEvent, ChangeEvent } from "react";
import useAuth from "../../../../../hooks/useAuth";
import { AppUserDto } from "../../../../../models/AppUserDto";
import { ApiResponseDto } from "../../../../../models/common/ApiResponseDto";
import { MessageResponseDto } from "../../../../../models/common/MessageResponseDto";
import { LoginAttemptDto } from "../../../../../models/LoginAttemptDto";
import localStyles from "../../AuthForm.module.css";
import requests from "../../../../../api/agent";
import ForgotPassword from "./ForgotPassword";

interface Props {
    onClose: () => void;
}

export default function LoginForm(props: Props) {
    const auth = useAuth();

    const [loginAttempt, setLoginAttempt] = useState<LoginAttemptDto>({
        credentials: "",
        password: "",
    });
    const [message, setMessage] = useState<MessageResponseDto>();

    const handleLogin = async (loginAttempt: LoginAttemptDto) => {
        const response: ApiResponseDto = await auth.login(loginAttempt);
        const user: AppUserDto = response.data;

        if (user && user.username) props.onClose();
        if (response.message) setMessage(response.message);
    };

    const handleFormSubmit = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        handleLogin(loginAttempt);
    };

    return (
        <>
            <form
                className={localStyles.formContent}
                onSubmit={handleFormSubmit}
            >
                <TextField
                    fullWidth
                    autoFocus
                    label="Email/Korisničko ime"
                    name="credentials"
                    value={loginAttempt.credentials}
                    onChange={(e: ChangeEvent<HTMLInputElement>) =>
                        setLoginAttempt((prev: LoginAttemptDto) => ({
                            ...prev,
                            credentials: e.target.value,
                        }))
                    }
                />
                <TextField
                    fullWidth
                    label="Lozinka"
                    name="password"
                    type="password"
                    value={loginAttempt.password}
                    onChange={(e: ChangeEvent<HTMLInputElement>) =>
                        setLoginAttempt((prev: LoginAttemptDto) => ({
                            ...prev,
                            password: e.target.value,
                        }))
                    }
                    sx={{ fontFamily: "Poppins, Arial, sans-serif" }}
                />
                <ForgotPassword />
                <Button
                    className={localStyles.myButton}
                    variant="contained"
                    type="submit"
                >
                    Login
                </Button>
            </form>
            {message && (
                <Alert severity={message.severity}>{message.message}</Alert>
            )}
        </>
    );
}
