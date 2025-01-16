import { Card, CardContent, Typography, Box, IconButton } from "@mui/material";
import UserLink from "../profil/UserLink";
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
                width: "32%",
                height: "400px",
                borderRadius: "10px",
                boxSizing: "border-box",
                border: "1px solid lightgray",
                minWidth: "300px",
            }}
        >
            <CardContent
                sx={{
                    textAlign: "center",
                    display: "flex",
                    flexDirection: "column",
                    justifyContent: "space-between",
                    gap: 1,
                    height: "100%",
                }}
            >
                <Box
                    display={"flex"}
                    flexDirection={"row"}
                    justifyContent={"space-between"}
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
                        sx={{ border: "1px solid #3a7ca5" }}
                    >
                        <KeyboardArrowRightIcon />
                    </IconButton>
                </Box>
                <Box
                    justifyContent={"flex-end"}
                    display="flex"
                    flexDirection={"row"}
                    alignItems="center"
                    gap={2}
                >
                    <UserLink
                        user={{
                            id: props.radionica.vlasnikId,
                            username: props.radionica.vlasnikUsername,
                            profilnaSlikaVersion:
                                props.radionica.vlasnikProfilnaSlikaVersion,
                        }}
                    />
                    <Box
                        display={"flex"}
                        alignItems={"flex-end"}
                        flexDirection={"row"}
                        gap={0.7}
                    >
                        {/* <ThumbUpIcon color="action" fontSize="small" /> */}
                    </Box>
                </Box>
            </CardContent>
        </Card>
    );
}
