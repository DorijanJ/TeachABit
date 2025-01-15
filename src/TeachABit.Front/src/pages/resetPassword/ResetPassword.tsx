import { useLocation } from "react-router-dom";
import requests from "../../api/agent";
import { useState } from "react";
import { MessageResponseDto } from "../../models/common/MessageResponseDto";
import { Alert, Button, InputAdornment, Stack, TextField } from "@mui/material";
import { Visibility, VisibilityOff } from "@mui/icons-material";

export default function ResetPassword() {
    const location = useLocation();
    const queryParams = new URLSearchParams(location.search);
    const email = queryParams.get("email");
    const token = queryParams.get("token");

    const [newPassword, setNewPassword] = useState<string>("");
    const [confirmPassword, setConfirmPassword] = useState<string>("");
    const [message, setMessage] = useState<MessageResponseDto>();
    const [showNewPassword, setShowNewPassword] = useState(false);
    const [showConfirmPassword, setShowConfirmPassword] = useState(false);

    const handleReset = async () => {
        if (email && token) {
            const response = await requests.postWithLoading(
                "account/reset-password",
                {
                    email: email,
                    token: token,
                    password: newPassword,
                }
            );
            if (response?.message) setMessage(response.message);
        }
    };

    return (
        <>
            <form
                style={{
                    backgroundColor: "background",
                    padding: "20px",
                    boxSizing: "border-box",
                    maxWidth: "fit-content",
                    borderRadius: "5px",
                    height: "300px",
                }}
                onSubmit={(e) => {
                    e.preventDefault();
                    handleReset();
                }}
            >
                <Stack
                    gap={"10px"}
                    padding={"10px"}
                    alignItems={"center"}
                    width={400}
                >
                    <TextField
                        fullWidth
                        label="New password"
                        value={newPassword}
                        type={showNewPassword ? "text" : "password"}
                        onChange={(e) => setNewPassword(e.target.value)}
                        slotProps={{
                            input: {
                                endAdornment: (
                                    <InputAdornment
                                        sx={{ cursor: "pointer" }}
                                        onClick={() =>
                                            setShowNewPassword((prev) => !prev)
                                        }
                                        position="end"
                                    >
                                        {showNewPassword ? (
                                            <Visibility />
                                        ) : (
                                            <VisibilityOff />
                                        )}
                                    </InputAdornment>
                                ),
                            },
                        }}
                    />
                    <TextField
                        fullWidth
                        label="Confirm password"
                        value={confirmPassword}
                        type={showConfirmPassword ? "text" : "password"}
                        onChange={(e) => setConfirmPassword(e.target.value)}
                        slotProps={{
                            input: {
                                endAdornment: (
                                    <InputAdornment
                                        sx={{ cursor: "pointer" }}
                                        onClick={() =>
                                            setShowConfirmPassword(
                                                (prev) => !prev
                                            )
                                        }
                                        position="end"
                                    >
                                        {showConfirmPassword ? (
                                            <Visibility />
                                        ) : (
                                            <VisibilityOff />
                                        )}
                                    </InputAdornment>
                                ),
                            },
                        }}
                    />
                    <Button
                        disabled={
                            !newPassword || newPassword !== confirmPassword
                        }
                        sx={{ width: "50%" }}
                        variant="contained"
                        type="submit"
                    >
                        {"Reset password"}
                    </Button>
                    {message && (
                        <Alert severity={message.severity}>
                            {message.message}
                        </Alert>
                    )}
                </Stack>
            </form>
        </>
    );
}
