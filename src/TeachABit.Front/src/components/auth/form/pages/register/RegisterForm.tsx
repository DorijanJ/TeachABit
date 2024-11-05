import { TextField, Button, Alert } from "@mui/material";
import { useState, FormEvent, ChangeEvent } from "react";
import useAuth from "../../../../../hooks/useAuth";
import { ApiResponseDto } from "../../../../../models/common/ApiResponseDto";
import { MessageResponseDto } from "../../../../../models/common/MessageResponseDto";
import { RegisterAttemptDto } from "../../../../../models/RegistetAttemptDto";
import localStyles from "../../AuthForm.module.css";

export default function RegisterForm() {
    const auth = useAuth();

    const [registerAttempt, setRegisterAttempt] = useState<RegisterAttemptDto>({
        email: "",
        username: "",
        password: "",
    });

    const [message, setMessage] = useState<MessageResponseDto>();

    const handleRegister = async (registerAttempt: RegisterAttemptDto) => {
        const response: ApiResponseDto = await auth.register(registerAttempt);
        if (response.message) setMessage(response.message);
    };

    const handleFormSubmit = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        handleRegister(registerAttempt);
    };

    return (
        <>
            <form
                className={localStyles.formContent}
                onSubmit={handleFormSubmit}
            >
                <TextField
                    fullWidth
                    label="Korisničko ime"
                    name="username"
                    value={registerAttempt.username}
                    onChange={(e: ChangeEvent<HTMLInputElement>) =>
                        setRegisterAttempt((prev: RegisterAttemptDto) => ({
                            ...prev,
                            username: e.target.value,
                        }))
                    }
                />
                <TextField
                    fullWidth
                    label="Email"
                    name="email"
                    value={registerAttempt.email}
                    onChange={(e: ChangeEvent<HTMLInputElement>) =>
                        setRegisterAttempt((prev: RegisterAttemptDto) => ({
                            ...prev,
                            email: e.target.value,
                        }))
                    }
                />
                <TextField
                    fullWidth
                    label="Lozinka"
                    name="password"
                    type="password"
                    value={registerAttempt.password}
                    onChange={(e: ChangeEvent<HTMLInputElement>) =>
                        setRegisterAttempt((prev: RegisterAttemptDto) => ({
                            ...prev,
                            password: e.target.value,
                        }))
                    }
                />
                <Button sx={{ width: 175 }} variant="contained" type="submit">
                    Registriraj se
                </Button>
            </form>
            {message && (
                <Alert severity={message.severity}>{message.message}</Alert>
            )}
        </>
    );
}
