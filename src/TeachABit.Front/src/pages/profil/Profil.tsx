import { useNavigate } from "react-router-dom";
import { useGlobalContext } from "../../context/Global.context";
import { Card, CardContent, Typography, Avatar } from "@mui/material";

export default function Profil() {
    const globalContext = useGlobalContext();
    const navigate = useNavigate();

    if (!globalContext.loggedInUser) {
        navigate("/");
        return <></>;
    }

    return (
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
                    {globalContext.loggedInUser.username[0]}
                </Avatar>
                <Typography variant="h4" align="center">
                    Korisnički profil
                </Typography>
                <Typography variant="h6" sx={{ marginBottom: 1 }}>
                    <strong>Username:</strong>{" "}
                    {globalContext.loggedInUser.username}
                </Typography>
            </CardContent>
        </Card>
    );
}
