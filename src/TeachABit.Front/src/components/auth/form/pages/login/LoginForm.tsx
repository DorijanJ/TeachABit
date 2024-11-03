import { TextField, Button, Alert } from "@mui/material";
import { useState, FormEvent, ChangeEvent } from "react";
import useAuth from "../../../../../hooks/useAuth";
import { AppUserDto } from "../../../../../models/AppUserDto";
import { ApiResponseDto } from "../../../../../models/common/ApiResponseDto";
import { MessageResponseDto } from "../../../../../models/common/MessageResponseDto";
import { LoginAttemptDto } from "../../../../../models/LoginAttemptDto";
import localStyles from "../../AuthForm.module.css";

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
        try {
            const response: ApiResponseDto = await auth.login(loginAttempt);
            const user: AppUserDto = response.data;

            if (user && user.username) props.onClose();
            if (response.message) setMessage(response.message);
        } catch (error: any) {
            const message: MessageResponseDto = error.message;
            setMessage(message);
        }
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
                    label="Emal/Username"
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
                    label="Password"
                    name="password"
                    type="password"
                    value={loginAttempt.password}
                    onChange={(e: ChangeEvent<HTMLInputElement>) =>
                        setLoginAttempt((prev: LoginAttemptDto) => ({
                            ...prev,
                            password: e.target.value,
                        }))
                    }
                />
                <Button sx={{ width: 150 }} variant="contained" type="submit">
                    Login
                </Button>
            </form>
            {message && (
                <Alert severity={message.severity}>{message.message}</Alert>
            )}
        </>
    );
}
