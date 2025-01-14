import {
    Dialog,
    DialogTitle,
    DialogContent,
    TextField,
    Alert,
    DialogActions,
    Button,
    Link,
} from "@mui/material";
import { ChangeEvent, useState } from "react";
import requests from "../../../../../api/agent";
import { MessageResponseDto } from "../../../../../models/common/MessageResponseDto";

export default function ForgotPassword() {
    const [message, setMessage] = useState<MessageResponseDto>();
    const [passwordResetEmail, setPasswordResetEmail] = useState<string>();
    const [isOpen, setIsOpen] = useState(false);

    const handleForgotPassword = async (email?: string) => {
        if (!email) return;
        const response = await requests.postWithLoading(
            "account/forgot-password",
            { email: email }
        );
        if (response && response.message) setMessage(response.message);
    };

    return (
        <>
            <Link
                type="button"
                component="button"
                onClick={() => {
                    setIsOpen(true);
                }}
            >
                Zaboravljena lozinka?
            </Link>
            {isOpen && (
                <Dialog
                    open={isOpen}
                    onClose={() => {
                        setPasswordResetEmail(undefined);
                        setIsOpen(false);
                    }}
                >
                    <DialogTitle>Upište email adresu</DialogTitle>
                    <DialogContent
                        sx={{
                            width: 450,
                            display: "flex",
                            flexDirection: "column",
                            gap: "20px",
                        }}
                    >
                        <TextField
                            fullWidth
                            autoFocus
                            label="Email"
                            variant="standard"
                            value={passwordResetEmail || ""}
                            onChange={(e: ChangeEvent<HTMLInputElement>) =>
                                setPasswordResetEmail(e.target.value)
                            }
                        />
                        {message && (
                            <Alert
                                sx={{ maxWidth: 370 }}
                                severity={message.severity}
                            >
                                {message.message}
                            </Alert>
                        )}
                    </DialogContent>
                    <DialogActions>
                        <Button
                            onClick={() =>
                                handleForgotPassword(passwordResetEmail)
                            }
                        >
                            Reset password
                        </Button>
                    </DialogActions>
                </Dialog>
            )}
        </>
    );
}
