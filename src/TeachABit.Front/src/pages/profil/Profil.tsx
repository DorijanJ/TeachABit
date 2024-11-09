import { useParams } from "react-router-dom";
import { useGlobalContext } from "../../context/Global.context";
import { Card, CardContent, Typography, Avatar } from "@mui/material";
import { useEffect, useMemo, useState } from "react";
import requests from "../../api/agent";
import { AppUserDto } from "../../models/AppUserDto";

export default function Profil() {
    const { username } = useParams();
    const globalContext = useGlobalContext();

    const [user, setUser] = useState<AppUserDto>();

    const isCurrentUser = useMemo(() => {
        return globalContext.loggedInUser?.username === username;
    }, [globalContext.loggedInUser?.username, username]);

    const GetUserByUsername = async (username: string) => {
        const response = await requests.getWithLoading(
            `account/by-username/${username}`
        );
        if (response.data) {
            setUser(response.data);
        }
    };

    useEffect(() => {
        if (username) GetUserByUsername(username);
    }, [username]);

    return (
        user && (
            <Card sx={{ width: 400 }}>
                <CardContent
                    sx={{
                        display: "flex",
                        flexDirection: "column",
                        alignItems: "center",
                        gap: "10px",
                    }}
                >
                    <Avatar sx={{ width: 100, height: 100 }}>
                        <img
                            style={{
                                objectFit: "cover",
                                width: "100%",
                                height: "100%",
                            }}
                            src={`${import.meta.env.VITE_REACT_AWS_BUCKET}${
                                user.id
                            }`}
                        />
                    </Avatar>
                    <Typography variant="h4" align="center">
                        Korisnički profil
                    </Typography>
                    <Typography variant="h6" sx={{ marginBottom: 1 }}>
                        <b>Username:</b> {user.username}
                    </Typography>
                </CardContent>
            </Card>
        )
    );
}
