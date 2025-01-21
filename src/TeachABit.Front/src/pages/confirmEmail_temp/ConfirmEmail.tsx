import { useLocation, useNavigate } from "react-router-dom";
import requests from "../../api/agent";
import { useEffect, useRef, useState } from "react";
import { MessageResponseDto } from "../../models/common/MessageResponseDto";
import { Alert } from "@mui/material";

export default function ConfirmEmail() {
    const location = useLocation();

    const queryParams = new URLSearchParams(location.search);
    const email = queryParams.get("email");
    const token = queryParams.get("token");

    const [message, setMessage] = useState<MessageResponseDto>();

    const sentRequest = useRef(false);

    const handleConfirmation = async () => {
        if (email && token && !sentRequest.current) {
            sentRequest.current = true;
            const response = await requests.postWithLoading(
                "account/confirm-email",
                { email, token }
            );
            if (response?.message) setMessage(response.message);
        }
    };

    const navigate = useNavigate();

    useEffect(() => {
        handleConfirmation();
    }, [token, email]);

    return (
        <>
            <div
                style={{
                    display: "flex",
                    gap: "20px",
                    flexDirection: "column",
                    alignItems: "center",
                }}
            >
                <img
                    onClick={() => navigate("/tecajevi")}
                    style={{ height: "200px", width: "200px" }}
                    src="/src/images/logo.png"
                />
                {message && (
                    <Alert severity={message.severity}>{message.message}</Alert>
                )}
            </div>
        </>
    );
}
