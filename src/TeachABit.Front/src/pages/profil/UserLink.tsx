import { Avatar, Typography } from "@mui/material";
import { useNavigate } from "react-router-dom";
import { AppUserDto } from "../../models/AppUserDto";

interface Props {
    user: AppUserDto;
}

export default function UserLink(props: Props) {
    const navigate = useNavigate();

    return (
        <div
            onClick={() => navigate(`/profil/${props.user.username}`)}
            style={{
                padding: "8px",
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
                {props.user.profilnaSlikaVersion ? (
                    <img
                        style={{
                            objectFit: "cover",
                            height: "100%",
                            width: "100%",
                        }}
                        src={`${import.meta.env.VITE_REACT_AWS_BUCKET}${
                            props.user.id
                        }${"?version=" + props.user.profilnaSlikaVersion}`}
                    ></img>
                ) : (
                    <>{props.user.username ? props.user.username[0] : ""}</>
                )}
            </Avatar>
            <Typography lineHeight={1} variant="caption">
                {props.user.username}
            </Typography>
        </div>
    );
}
