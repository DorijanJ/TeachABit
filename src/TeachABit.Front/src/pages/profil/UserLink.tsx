import { Avatar, Typography } from "@mui/material";
import { useNavigate } from "react-router-dom";

interface Props {
    userId?: string;
    username?: string;
}

export default function UserLink(props: Props) {
    const navigate = useNavigate();

    return (
        <div
            onClick={() => navigate(`/profil/${props.username}`)}
            style={{
                padding: "5px",
                borderRadius: "3px",
                backgroundColor: "#f1f1f1",
                display: "flex",
                flexDirection: "row",
                alignItems: "center",
                gap: "5px",
                cursor: "pointer",
            }}
        >
            <Avatar sx={{ width: 20, height: 20 }}>
                <img
                    style={{
                        objectFit: "cover",
                        height: "100%",
                        width: "100%",
                    }}
                    src={`${import.meta.env.VITE_REACT_AWS_BUCKET}${
                        props.userId
                    }`}
                ></img>
            </Avatar>
            <Typography lineHeight={1} variant="caption">
                {props.username}
            </Typography>
        </div>
    );
}
