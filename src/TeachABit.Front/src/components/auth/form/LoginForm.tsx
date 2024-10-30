import { ChangeEvent, FormEvent, useState } from "react";
import { observer } from "mobx-react";
import useAuth from "../../../hooks/useAuth";
import { useNavigate } from "react-router-dom";
import { LoginAttemptDto } from "../../../models/LoginAttemptDto";
import { AppUserDto } from "../../../models/AppUserDto";
import { Button, TextField } from "@mui/material";
import localStyles from "./AuthForm.module.css";
import { useGlobalContext } from "../../../context/Global.context";

const LoginForm = observer(() => {
    const auth = useAuth();

    const globalContext = useGlobalContext();

    const [loginAttempt, setLoginAttempt] = useState<LoginAttemptDto>({
        credentials: "",
        password: "",
    });

    const handleLogin = async (loginAttempt: LoginAttemptDto) => {
        const authResult: AppUserDto = await auth.login(loginAttempt);
        if (authResult.username) {
            globalContext.setIsOpenAuthForm(false);
        }
    };

    const handleFormSubmit = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        handleLogin(loginAttempt);
    };

    return (
        <form className={localStyles.formContent} onSubmit={handleFormSubmit}>
            <TextField
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
            <Button variant="contained" type="submit">
                Login
            </Button>
        </form>
    );
});

export default LoginForm;
