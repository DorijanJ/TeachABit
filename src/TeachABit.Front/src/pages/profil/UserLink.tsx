import { Avatar, Typography } from "@mui/material";
import { useNavigate } from "react-router-dom";
import { AppUserDto } from "../../models/AppUserDto";

interface Props {
    user: AppUserDto;
    width?: number | string;
    fontSize?: number | string;
}

export default function UserLink(props: Props) {
    const navigate = useNavigate();

    return (
        <div
            onClick={() => navigate(`/profil/${props.user.username}`)}
            style={{
                padding: "4px 6px",
                borderRadius: "3px",
                backgroundColor: "#f1f1f1",
                display: "flex",
                flexDirection: "row",
                alignItems: "center",
                gap: "5px",
                cursor: "pointer",
                justifyContent: "center",
                width: props.width ?? "auto"
            }}
        >
            <Avatar sx={{ width: 25, height: 25 }}>
                {props.user.profilnaSlikaVersion ? (
                    <img
                        style={{
                            objectFit: "cover",
                            height: "100%",
                            width: "100%",
                        }}
                        src={`${import.meta.env.VITE_REACT_AWS_BUCKET}${props.user.id
                            }${"?version=" + props.user.profilnaSlikaVersion}`}
                    ></img>
                ) : (
                    <>{props.user.username ? props.user.username[0] : ""}</>
                )}
            </Avatar>
            <Typography lineHeight={1} variant="caption" fontSize={props.fontSize ?? "auto"}>
                {props.user.username}
            </Typography>
        </div>
    );
}
