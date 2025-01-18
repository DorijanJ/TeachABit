import { Card, CardContent, Typography, Box, IconButton } from "@mui/material";
import { ObjavaDto } from "../../models/ObjavaDto";
import UserLink from "../profil/UserLink";
import ThumbUpIcon from "@mui/icons-material/ThumbUp";
import { useNavigate } from "react-router-dom";
interface Props {
    objava: ObjavaDto;
}

export default function Objava(props: Props) {
    const navigate = useNavigate();

    return (
        <Card
            sx={{
                width: "100%",
                borderRadius: "10px",
                boxSizing: "border-box",
                border: "1px solid lightgray",
                minWidth: "300px",
            }}
            id="objava"
        >
            <CardContent
                sx={{
                    textAlign: "center",
                    display: "flex",
                    flexDirection: "row",
                    justifyContent: "space-between",
                    gap: 1,
                }}
            >
                <Typography
                    color="primary"
                    variant="h5"
                    component="div"
                    id="objavaNaziv"
                    sx={{
                        textOverflow: "ellipsis",
                        overflow: "hidden",
                        whiteSpace: "nowrap",
                        maxWidth: "100%",
                        color: "black",
                        textDecoration: "underline",
                        textDecorationColor: "gray",
                        cursor: "pointer",
                    }}
                    onClick={() => navigate(`/objava/${props.objava.id}`)}
                >
                    {props.objava.naziv}
                </Typography>
            </CardContent>
            <Box
                m={2}
                justifyContent={"space-between"}
                display="flex"
                flexDirection={"row"}
                alignItems="center"
                gap={2}
            >
                <UserLink
                    user={{
                        id: props.objava.vlasnikId,
                        username: props.objava.vlasnikUsername,
                        profilnaSlikaVersion:
                            props.objava.vlasnikProfilnaSlikaVersion,
                    }}
                />
                <Box
                    display={"flex"}
                    alignItems={"flex-end"}
                    flexDirection={"row"}
                    gap={0.7}
                >
                    <p
                        style={{
                            margin: 0,
                            display: "inline",
                            lineHeight: "18px",
                        }}
                    >
                        {props.objava.likeCount}
                    </p>
                    <ThumbUpIcon color="action" fontSize="small" />
                </Box>
            </Box>
        </Card>
    );
}
