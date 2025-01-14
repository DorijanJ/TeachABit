import { TextField, Button, Alert } from "@mui/material";
import { useState, FormEvent, ChangeEvent } from "react";
import useAuth from "../../../../../hooks/useAuth";
import { AppUserDto } from "../../../../../models/AppUserDto";
import { MessageResponseDto } from "../../../../../models/common/MessageResponseDto";
import { LoginAttemptDto } from "../../../../../models/LoginAttemptDto";
import localStyles from "../../AuthForm.module.css";
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
        const response = await auth.login(loginAttempt);
        if (!response) return;
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
                    color="secondary"
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
                    color="secondary"
                    value={loginAttempt.password}
                    onChange={(e: ChangeEvent<HTMLInputElement>) =>
                        setLoginAttempt((prev: LoginAttemptDto) => ({
                            ...prev,
                            password: e.target.value,
                        }))
                    }
                />
                <ForgotPassword />
                <Button
                    className={localStyles.myButton}
                    variant="contained"
                    type="submit"
                    color="secondary"
                >
                    Prijava
                </Button>
            </form>
            {message && (
                <Alert severity={message.severity}>{message.message}</Alert>
            )}
        </>
    );
}
