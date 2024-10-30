import { observer } from "mobx-react";
import useAuth from "../../../hooks/useAuth";
import { ChangeEvent, FormEvent, useState } from "react";
import { useNavigate } from "react-router-dom";
import { RegisterAttemptDto } from "../../../models/RegistetAttemptDto";
import { AppUserDto } from "../../../models/AppUserDto";
import { Button, TextField } from "@mui/material";
import localStyles from "./AuthForm.module.css";
import { useGlobalContext } from "../../../context/Global.context";

const RegisterForm = observer(() => {
    const auth = useAuth();

    const globalContext = useGlobalContext();

    const [registerAttempt, setRegisterAttempt] = useState<RegisterAttemptDto>({
        email: "",
        username: "",
        password: "",
    });

    const handleRegister = async (registerAttempt: RegisterAttemptDto) => {
        const authResult: AppUserDto = await auth.register(registerAttempt);
        if (authResult.username) {
            globalContext.setIsOpenAuthForm(false);
        }
    };

    const handleFormSubmit = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        handleRegister(registerAttempt);
    };

    return (
        <form className={localStyles.formContent} onSubmit={handleFormSubmit}>
            <TextField
                label="Username"
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
                label="Password"
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
            <Button variant="contained" type="submit">
                Register
            </Button>
        </form>
    );
});

export default RegisterForm;
