import { useParams } from "react-router-dom";
import { useGlobalContext } from "../../context/Global.context";
import { Card, CardContent, Typography, Avatar, Button } from "@mui/material";
import { useEffect, useMemo, useState } from "react";
import requests from "../../api/agent";
import { AppUserDto } from "../../models/AppUserDto";

import EditProfilDialog from "./EditProfilDialog";

export default function Profil() {
    const { username } = useParams();
    const globalContext = useGlobalContext();

    const [user, setUser] = useState<AppUserDto>();

    const isCurrentUser = useMemo(() => {
        return globalContext.currentUser?.username === username;
    }, [globalContext.currentUser?.username, username]);

    const GetUserByUsername = async (username: string) => {
        const response = await requests.getWithLoading(
            `account/by-username/${username}`
        );
        if (response && response.data) {
            setUser(response.data);
        }
    };

    const setupStripeAccount = async () => {
        const response = await requests.getWithLoading(
            "placanja/napravi-stripe-account"
        );
        if (response && response.data) {
            window.location.href = response.data.url;
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
                        {user.id && user.profilnaSlikaVersion ? (
                            <>
                                <img
                                    style={{
                                        objectFit: "cover",
                                        width: "100%",
                                        height: "100%",
                                    }}
                                    src={`${
                                        import.meta.env.VITE_REACT_AWS_BUCKET
                                    }${user.id}${
                                        user.profilnaSlikaVersion
                                            ? "?version=" +
                                              user.profilnaSlikaVersion
                                            : ""
                                    }`}
                                />
                            </>
                        ) : (
                            <>{user.username ? user.username[0] : ""}</>
                        )}
                    </Avatar>
                    {globalContext.userIsLoggedIn === true && isCurrentUser && (
                        <EditProfilDialog
                            onClose={() => {
                                if (username) GetUserByUsername(username);
                            }}
                        />
                    )}
                    <Typography variant="h4" align="center">
                        Korisnički profil
                    </Typography>
                    <Typography variant="h6" sx={{ marginBottom: 1 }}>
                        <b>Username:</b> {user.username}
                    </Typography>
                    {user.roles &&
                        user.roles.length > 0 &&
                        user.roles.map((role) => <p>{role}</p>)}
                </CardContent>
                <Button onClick={setupStripeAccount}>Spoji Plaćanje</Button>
            </Card>
        )
    );
}
