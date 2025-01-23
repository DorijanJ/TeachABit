import { Avatar } from "@mui/material";
import { useNavigate } from "react-router-dom";
import { AppUserDto } from "../../models/AppUserDto";
import { useState } from "react";

interface Props {
    user: AppUserDto;
    width?: number | string;
    fontSize?: number | string;
    withBackground?: boolean;
}

export default function UserLink(props: Props) {
    const navigate = useNavigate();
    const [withBackground] = useState<boolean>(props.withBackground ?? false);

    return (
        <div
            onClick={() => navigate(`/profil/${props.user.username}`)}
            style={{
                padding: "8px 14px",
                borderRadius: "3px",
                backgroundColor: withBackground ? "#f1f1f1" : "transparent",
                display: "inline-flex",
                flexDirection: "row",
                alignItems: "center",
                gap: "5px",
                cursor: "pointer",
                minWidth: "fitContent",
                maxWidth: "180px",
                height: "40px",
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
                <Avatar sx={{ width: 25, height: 25 }}>
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
                    }}
                >
                    {props.user.username}
                </p>
            </div>
        </div>
    );
}
