import { Avatar } from "@mui/material";
import { useNavigate } from "react-router-dom";
import { AppUserDto } from "../../models/AppUserDto";

interface Props {
    user: AppUserDto;
    width?: number | string;
    height?: number | string;
    fontSize?: number;
}

export default function UserLink(props: Props) {
    const navigate = useNavigate();

    return (
        <div
            onClick={() => navigate(`/profil/${props.user.username}`)}
            style={{
                padding: "8px 14px",
                borderRadius: "3px",
                backgroundColor: "transparent",
                display: "inline-flex",
                flexDirection: "row",
                alignItems: "center",
                gap: "5px",
                cursor: "pointer",
                width: props.width ?? "auto",
                minWidth: "fitContent",
                maxWidth: "180px",
                height: props.height ?? "40px",
            }}
        >
            <div
                style={{
                    display: "flex",
                    flexDirection: "row",
                    alignItems: "center",
                    gap: "5px",
                    maxWidth: "100%",
                }}
            >
                <Avatar
                    sx={{
                        width: props.fontSize ? props.fontSize * 2 : 25,
                        height: props.fontSize ? props.fontSize * 2 : 25,
                    }}
                >
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
                <p
                    style={{
                        margin: 0,
                        maxWidth: "100%",
                        overflow: "hidden",
                        textOverflow: "ellipsis",
                        fontSize: props.fontSize ?? "auto",
                    }}
                >
                    {props.user.username}
                </p>
            </div>
        </div>
    );
}
