import { GoogleOAuthProvider, GoogleLogin } from "@react-oauth/google";
import { useState, FormEvent, ChangeEvent } from "react";
import { MessageCodes } from "../../../../../enums/MessageCodes";
import { AppUserDto } from "../../../../../models/AppUserDto";
import { ApiResponseDto } from "../../../../../models/common/ApiResponseDto";
import { MessageResponseDto } from "../../../../../models/common/MessageResponseDto";
import {
    Dialog,
    TextField,
    Button,
    Alert,
    DialogTitle,
    DialogActions,
    DialogContent,
} from "@mui/material";
import { useGlobalContext } from "../../../../../context/Global.context";
import useAuth from "../../../../../hooks/useAuth";

interface GoogleAuthData {
    token: string;
    username?: string;
}

interface Props {
    onClose: () => void;
}

export default function GoogleAuth(props: Props) {
    const [authData, setAuthData] = useState<GoogleAuthData | null>(null);
    const [isUsernameDialogOpen, setIsUsernameDialogOpen] = useState(false);
    const [alertMessage, setAlertMessage] = useState<MessageResponseDto | null>(
        null
    );
    const auth = useAuth();

    const handleGoogleSuccess = async (token: string) => {
        const authRequest: GoogleAuthData = { token };
        setAuthData(authRequest);
        handleGoogleLogin(authRequest);
    };

    const handleGoogleLogin = async (authRequest: GoogleAuthData) => {
        const response: ApiResponseDto = await auth.loginGoogle(authRequest);
        const user: AppUserDto = response.data;
        if (user?.username) props.onClose();

        if (response.message) {
            if (isUsernameDialogOpen) setAlertMessage(response.message);
            else if (
                response.message.code === MessageCodes.UsernameNotProvided
            ) {
                setIsUsernameDialogOpen(true);
                setAuthData((prev: any) => ({
                    ...prev,
                    username: "",
                }));
            }
        }
    };

    const handleUsernameFormSubmit = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        if (authData) handleGoogleLogin(authData);
    };

    const handleCloseDialog = () => {
        setIsUsernameDialogOpen(false);
        setAuthData(null);
    };

    return (
        <GoogleOAuthProvider
            clientId={import.meta.env.VITE_REACT_GOOGLE_CLIENT}
        >
            <GoogleLogin
                onSuccess={(response) => {
                    if (response.credential)
                        handleGoogleSuccess(response.credential);
                }}
            />
            {isUsernameDialogOpen && (
                <Dialog
                    open={isUsernameDialogOpen}
                    onClose={handleCloseDialog}
                    PaperProps={{
                        component: "form",
                        onSubmit: handleUsernameFormSubmit,
                    }}
                >
                    <DialogTitle>
                        Choose a username to create your account!
                    </DialogTitle>
                    <DialogContent
                        sx={{
                            width: 400,
                            display: "flex",
                            flexDirection: "column",
                            gap: "20px",
                        }}
                    >
                        <TextField
                            fullWidth
                            autoFocus
                            label="Username"
                            name="username"
                            variant="standard"
                            value={authData?.username || ""}
                            onChange={(e: ChangeEvent<HTMLInputElement>) =>
                                setAuthData((prev) => ({
                                    ...prev!,
                                    username: e.target.value ?? "",
                                }))
                            }
                        />
                        {alertMessage && (
                            <Alert
                                sx={{ maxWidth: 370 }}
                                severity={alertMessage.severity}
                            >
                                {alertMessage.message}
                            </Alert>
                        )}
                    </DialogContent>
                    <DialogActions>
                        <Button onClick={handleCloseDialog}>Cancel</Button>
                        <Button variant="contained" type="submit">
                            Create Account
                        </Button>
                    </DialogActions>
                </Dialog>
            )}
        </GoogleOAuthProvider>
    );
}
