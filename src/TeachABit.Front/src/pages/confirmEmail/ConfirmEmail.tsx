import { useLocation } from "react-router-dom";
import requests from "../../api/agent";
import { useEffect, useRef, useState } from "react";
import { MessageResponseDto } from "../../models/common/MessageResponseDto";
import { ApiResponseDto } from "../../models/common/ApiResponseDto";
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
            const response: ApiResponseDto = await requests.postWithLoading(
                "account/confirm-email",
                { email, token }
            );
            if (response.message) setMessage(response.message);
        }
    };

    useEffect(() => {
        handleConfirmation();
    }, [token, email]);

    return (
        <>
            {message && (
                <Alert severity={message.severity}>{message.message}</Alert>
            )}
        </>
    );
}
