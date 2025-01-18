import { useEffect, useState } from "react";
import { TecajDto } from "../../models/TecajDto";
import { useNavigate, useParams } from "react-router-dom";
import requests from "../../api/agent";
import { useGlobalContext } from "../../context/Global.context";
import {
    Box,
    Breadcrumbs,
    Card,
    CardContent,
    IconButton,
    Link,
    Typography,
} from "@mui/material";
import UserLink from "../profil/UserLink";
import DeleteIcon from "@mui/icons-material/Delete";

export default function TecajPage() {
    const [tecaj, setTecaj] = useState<TecajDto>({
        naziv: "",
        opis: "",
    });

    const { tecajId } = useParams();

    useEffect(() => {
        if (tecajId) {
            getTecajById(parseInt(tecajId));
        }
    }, [tecajId]);

    const getTecajById = async (tecajId: number) => {
        const response = await requests.getWithLoading(`tecajevi/${tecajId}`);
        if (response?.data) {
            setTecaj(response.data);
        } else {
            navigate("/tecajevi");
        }
    };

    const navigate = useNavigate();
    const globalContext = useGlobalContext();

    const deleteTecaj = async () => {
        const response = await requests.deleteWithLoading(
            `tecajevi/${tecajId}`
        );
        if (response && response.message?.severity === "success")
            navigate("/tecajevi");
    };

    return (
        <>
            <Card
                sx={{
                    width: "100%",
                    height: "100%",
                    overflowY: "auto",
                    scrollbarGutter: "stable",
                }}
            >
                <Breadcrumbs aria-label="breadcrumb" sx={{ padding: "15px" }}>
                    <Link
                        underline="hover"
                        color="inherit"
                        onClick={() => navigate("/tecajevi")}
                    >
                        Tečajevi
                    </Link>

                    <Typography sx={{ color: "text.primary" }}>
                        {tecaj.id}
                    </Typography>
                </Breadcrumbs>
                <CardContent
                    sx={{
                        display: "flex",
                        flexDirection: "column",
                        gap: "20px",
                        paddingTop: "10px !important",
                        width: "100%",
                    }}
                >
                    <div
                        style={{
                            width: "100%",
                            display: "flex",
                            justifyContent: "space-between",
                            alignItems: "center",
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
                            {tecaj.naziv}
                        </Typography>
                        <Box
                            flexDirection={"row"}
                            alignItems={"center"}
                            display={"flex"}
                            justifyContent={"space-between"}
                            gap="5px"
                        >
                            <UserLink
                                user={{
                                    id: tecaj.vlasnikId,
                                    username: tecaj.vlasnikUsername,
                                    profilnaSlikaVersion:
                                        tecaj.vlasnikProfilnaSlikaVersion,
                                }}
                            />
                        </Box>
                    </div>
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
                        {tecaj.opis}
                    </Typography>
                    <Box
                        display={"flex"}
                        flexDirection={"row"}
                        justifyContent={"flex-end"}
                        alignItems={"center"}
                        gap="10px"
                    >
                        {(globalContext.currentUser?.id === tecaj.vlasnikId ||
                            globalContext.isAdmin) && (
                            <>
                                <IconButton
                                    onClick={() => deleteTecaj()}
                                    sx={{
                                        width: "40px",
                                        height: "40px",
                                    }}
                                >
                                    <DeleteIcon color="primary"></DeleteIcon>
                                </IconButton>
                            </>
                        )}
                    </Box>
                </CardContent>
            </Card>
        </>
    );
}
