import { useLocation } from "react-router-dom";
import requests from "../../api/agent";
import { useEffect, useState } from "react";
import { MessageResponseDto } from "../../models/common/MessageResponseDto";
import { ApiResponseDto } from "../../models/common/ApiResponseDto";

export default function ConfirmEmail() {
    const location = useLocation();

    const queryParams = new URLSearchParams(location.search);
    const email = queryParams.get("email");
    const token = queryParams.get("token");

    const [message, setMessage] = useState<MessageResponseDto>();

    const handleConfirmation = async () => {
        if (email && token) {
            try {
                const response: ApiResponseDto = await requests.postWithLoading(
                    "account/confirm-email",
                    {
                        email: email,
                        token: token,
                    }
                );
                if (response.message) setMessage(response.message);
            } catch (error) {
                console.error("Error confirming email:", error);
            }
        }
    };

    useEffect(() => {
        handleConfirmation();
    }, [token, email]);

    return <>{message && <div>{message.message}</div>}</>;
}
