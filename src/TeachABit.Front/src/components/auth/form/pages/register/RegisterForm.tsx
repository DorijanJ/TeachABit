import { TextField, Button, Alert, Tooltip, IconButton, Typography, } from "@mui/material";
import HelpIcon from "@mui/icons-material/Help";
import { useState, FormEvent, ChangeEvent, useMemo } from "react";
import useAuth from "../../../../../hooks/useAuth";
import { MessageResponseDto } from "../../../../../models/common/MessageResponseDto";
import { RegisterAttemptDto } from "../../../../../models/RegistetAttemptDto";
import localStyles from "../../AuthForm.module.css";

const maxEmailLength = 50;
const maxUsernameLength = 50;
const minPasswordLength = 8;
const maxPasswordLength = 16;

export default function RegisterForm() {
    const auth = useAuth();

    const [registerAttempt, setRegisterAttempt] = useState<RegisterAttemptDto>({
        email: "",
        username: "",
        password: "",
    });

    const [message, setMessage] = useState<MessageResponseDto>();

    const handleRegister = async (registerAttempt: RegisterAttemptDto) => {
        const response = await auth.register(registerAttempt);
        if (response && response.message) setMessage(response.message);
    };

    const handleFormSubmit = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        handleRegister(registerAttempt);
    };

    const isValidEmail = useMemo(() => {
        const email = registerAttempt.email;
        if (email.length == 0 || email.length > maxEmailLength) return false;
        return true;
    }, [registerAttempt.email]);

    const isValidUsername = useMemo(() => {
        const username = registerAttempt.username;
        if (username.length == 0 || username.length > maxUsernameLength)
            return false;
        return true;
    }, [registerAttempt.username]);

    const isValidPassword = useMemo(() => {
        const password = registerAttempt.password;
        if (
            password.length < minPasswordLength ||
            password.length > maxPasswordLength ||
            !/[A-Z]/.test(password) ||
            !/[a-z]/.test(password) ||
            !/[0-9]/.test(password)
        )
            return false;
        return true;
    }, [registerAttempt.password]);

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
                    color="primary"
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
                    color="primary"
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
                    color="primary"
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
                    slotProps={{
                        input: {
                            endAdornment: (
                                <Tooltip
                                    title={
                                        <Typography variant="body2">
                                            Duljina 8-16 znakova.
                                            <br />
                                            Mora sadržavati barem jedno malo slovo.
                                            <br />
                                            Mora sadržavati barem jedno veliko slovo.
                                            <br />
                                            Mora sadržavati barem jedan broj.
                                        </Typography>
                                    }
                                >
                                    <IconButton>
                                        <HelpIcon />
                                    </IconButton>
                                </Tooltip>
                            ),
                        },
                    }}
                />
                <Button
                    className={localStyles.myButton}
                    disabled={
                        !isValidEmail || !isValidUsername || !isValidPassword
                    }
                    variant="contained"
                    type="submit"
                    color="primary"
                >
                    Registracija
                </Button>
            </form>
            {message && (
                <Alert severity={message.severity}>{message.message}</Alert>
            )}
        </>
    );
}
