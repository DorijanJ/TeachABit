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
import { ChangeEvent, useMemo, useState } from "react";
import requests from "../../../../../api/agent";
import { MessageResponseDto } from "../../../../../models/common/MessageResponseDto";

const maxEmailLength = 50;

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

    const isValidEmail = useMemo(() => {
        const email = passwordResetEmail ?? "";
        if (email.length == 0 || email.length > maxEmailLength) return false;
        return true;
    }, [passwordResetEmail]);

    return (
        <>
            <Link
                type="button"
                component="button"
                color="primary"
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
                            maxWidth: "100%",
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
                            disabled={!isValidEmail}
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
