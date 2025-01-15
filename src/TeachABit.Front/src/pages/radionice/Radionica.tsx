import { Card, CardContent, Typography, Box, IconButton } from "@mui/material";
import UserLink from "../profil/UserLink";
import ThumbUpIcon from "@mui/icons-material/ThumbUp";
import { useNavigate } from "react-router-dom";
import KeyboardArrowRightIcon from "@mui/icons-material/KeyboardArrowRight";
import { RadionicaDto } from "../../models/RadionicaDto";

interface Props {
    radionica: RadionicaDto;
}

export default function Radionica(props: Props) {
    const navigate = useNavigate();

    return (
        <Card
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
                    flexDirection: "row",
                    justifyContent: "space-between",
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
                    {props.radionica.naziv}
                </Typography>

                <IconButton
                    color="primary"
                    onClick={() => {
                        navigate(`/radionica/${props.radionica.id}`);
                    }}
                    size="small"
                    sx={{ border: "1px solid #922728" }}
                >
                    <KeyboardArrowRightIcon />
                </IconButton>
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
                        id: props.radionica.predavacId,
                        username: props.radionica.naziv,
                        profilnaSlikaVersion: props.radionica.predavacProfilnaSlika,
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
                        nesto nesto
                    </p>
                    <ThumbUpIcon color="action" fontSize="small" />
                </Box>
            </Box>
        </Card>
    );
}
