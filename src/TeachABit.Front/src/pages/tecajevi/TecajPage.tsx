import { Box, Card, CardContent, IconButton, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { TecajDto } from "../../models/TecajDto";
import { useNavigate, useParams } from "react-router-dom";
import requests from "../../api/agent";
import { useGlobalContext } from "../../context/Global.context";
import UserLink from "../profil/UserLink";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import TeachABitRenderer from "../../components/editor/TeachaBitRenderer";
import Lekcije from "./Lekcije";
import NavigateBeforeIcon from "@mui/icons-material/NavigateBefore";
import TecajKomentari from "./TecajKomentari";
import { LevelPristupa } from "../../enums/LevelPristupa";
import TecajPopup from "./TecajPopup";
import PotvrdiPopup from "../../components/dialogs/PotvrdiPopup";

export default function TecajPage() {
    const { tecajId } = useParams();
    const [tecaj, setTecaj] = useState<TecajDto>({
        id: tecajId ? parseInt(tecajId) : undefined,
        naziv: "",
        opis: "",
        cijena: undefined,
    });
    const navigate = useNavigate();
    const globalContext = useGlobalContext();

    /* otvaranje i zatvaranje prozora za uredivanje tecaja */
    const [popupOpen, setTecajDialogOpen] = useState(false);
    const handleTecajPopupOpen = () => setTecajDialogOpen(true);
    const handleTecajPopupClose = () => setTecajDialogOpen(false);

    /* potvrda za brisanje tecaja */
    const [isPotvrdaOpen, setIsPotvrdaOpen] = useState(false);

    useEffect(() => {
        fetchTecaj();
    }, [tecajId]);

    const fetchTecaj = async () => {
        if (tecajId) {
            const response = await requests.getWithLoading(
                `tecajevi/${parseInt(tecajId)}`
            );
            if (response?.data) {
                setTecaj(response.data);
            } else {
                navigate("/tecajevi");
            }
        }
    };

    const deleteTecaj = async () => {
        if (tecajId) {
            const response = await requests.deleteWithLoading(
                `tecajevi/${tecajId}`
            );
            if (response && response.message?.severity === "success")
                navigate(-1);
        }
    };

    return (
        <>
            <TecajPopup
                isOpen={popupOpen}
                onClose={handleTecajPopupClose}
                refreshData={fetchTecaj}
                tecaj={tecaj}
                editing={true}
            />
            {isPotvrdaOpen && (
                <PotvrdiPopup
                    onConfirm={() => deleteTecaj()}
                    onClose={() => setIsPotvrdaOpen(false)}
                    tekstPitanje="Jeste li sigurni da želite izbrisati tečaj?"
                    tekstOdgovor="Izbriši"
                />
            )}
            <Card
                sx={{
                    width: "100%",
                    height: "100%",
                    overflowY: "auto",
                    scrollbarGutter: "stable",
                }}
            >
                <Box
                    display={"flex"}
                    flexDirection={"row"}
                    justifyContent={"space-between"}
                    alignItems={"center"}
                    sx={{
                        display: "flex",
                        alignItems: "center",
                        margin: "10px",
                    }}
                >
                    <IconButton
                        onClick={() => navigate(-1)}
                        sx={{
                            color: "#3a7ca5",
                            "&:hover": {
                                color: "#1e4f72",
                            },
                        }}
                    >
                        <NavigateBeforeIcon
                            sx={{
                                fontSize: 30,
                            }}
                        />
                    </IconButton>
                    <UserLink
                        user={{
                            id: tecaj.vlasnikId,
                            username: tecaj.vlasnikUsername,
                            profilnaSlikaVersion:
                                tecaj.vlasnikProfilnaSlikaVersion,
                        }}
                        withBackground={false}
                    />
                </Box>

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
                            //justifyContent: "space-between",
                            alignItems: "center",
                            flexWrap: "wrap",
                        }}
                    >
                        <Typography
                            color="primary"
                            variant="h5"
                            component="div"
                            sx={{
                                wordWrap: "break-word",
                                maxWidth: "100%",
                                color: "black",
                            }}
                        >
                            {tecaj.naziv}
                        </Typography>

                        {(globalContext.currentUser?.id === tecaj.vlasnikId ||
                            globalContext.hasPermissions(
                                LevelPristupa.Moderator
                            )) && (
                                <Box
                                    display={"flex"}
                                    flexDirection={"row"}
                                    alignItems={"center"}
                                    marginLeft={1}
                                >
                                    <IconButton
                                        onClick={() => handleTecajPopupOpen()}
                                        sx={{
                                            width: "40px",
                                            height: "40px",
                                        }}
                                    >
                                        <EditIcon color="primary"></EditIcon>
                                    </IconButton>
                                    <IconButton
                                        onClick={() => setIsPotvrdaOpen(true)}
                                        sx={{
                                            width: "40px",
                                            height: "40px",
                                        }}
                                    >
                                        <DeleteIcon color="primary"></DeleteIcon>
                                    </IconButton>
                                </Box>
                            )}
                    </div>

                    {tecaj.naslovnaSlikaVersion && (
                        <div
                            style={{
                                width: "100%",
                                display: "flex",
                                flexDirection: "row",
                                justifyContent: "center",
                                paddingTop: "20px",
                            }}
                        >
                            <img
                                style={{
                                    borderRadius: "10px",
                                    objectFit: "cover",
                                    width: "70%",
                                }}
                                src={`${import.meta.env.VITE_REACT_AWS_BUCKET}${tecaj.naslovnaSlikaVersion
                                    }`}
                            />
                        </div>
                    )}

                    <TeachABitRenderer content={tecaj.opis} />

                    {/* Popis lekcija */}
                    {tecaj.lekcije && tecajId && (
                        <Lekcije
                            lekcije={tecaj.lekcije}
                            refreshData={fetchTecaj}
                            tecajId={parseInt(tecajId)}
                            vlasnikId={tecaj.vlasnikId}
                        />
                    )}

                    {tecaj.id && <TecajKomentari tecajId={tecaj.id} />}
                </CardContent>
            </Card>
        </>
    );
}
