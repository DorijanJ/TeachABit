import { Card, CardContent, Typography, Box, Avatar } from "@mui/material";
import { ObjavaDto } from "../../models/ObjavaDto";
import UserLink from "../profil/UserLink";

interface Props {
    objava: ObjavaDto;
    onClick: () => void;
}

export default function Objava(props: Props) {
    return (
        <Card
            onClick={props.onClick}
            sx={{
                width: "500px",
                borderRadius: "2px",
                boxSizing: "border-box",
                border: "1px solid transparent",
                "&:hover": {
                    border: "1px solid #922728",
                },
            }}
        >
            <CardContent
                sx={{
                    textAlign: "center",
                    display: "flex",
                    flexDirection: "column",
                    gap: 1,
                }}
            >
                <Typography
                    color="primary"
                    variant="h5"
                    component="div"
                    sx={{
                        textOverflow: "ellipsis",
                        overflow: "hidden",
                        whiteSpace: "nowrap",
                        maxWidth: "100%",
                    }}
                >
                    {props.objava.naziv}
                </Typography>
            </CardContent>
            <Box
                m={2}
                justifyContent={"flex-end"}
                display="flex"
                flexDirection={"row"}
                alignItems="center"
                gap={0.5}
            >
                <UserLink
                    user={{
                        id: props.objava.vlasnikId,
                        username: props.objava.vlasnikUsername,
                        profilnaSlikaVersion:
                            props.objava.vlasnikProfilnaSlikaVersion,
                    }}
                />
            </Box>
        </Card>
    );
}
