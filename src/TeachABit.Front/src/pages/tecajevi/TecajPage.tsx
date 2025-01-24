import { Box, Card, CardContent, IconButton, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { TecajDto } from "../../models/TecajDto";
import { useNavigate, useParams } from "react-router-dom";
import requests from "../../api/agent";
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
import globalStore from "../../stores/GlobalStore";
import FavoriteIcon from "@mui/icons-material/Favorite";
import TecajRating from "./TecajRating";

export default function TecajPage() {
    const { tecajId } = useParams();
    const [isLiked, setIsLiked] = useState(false);
    const [tecaj, setTecaj] = useState<TecajDto>({
        id: tecajId ? parseInt(tecajId) : undefined,
        naziv: "",
        opis: "",
        cijena: undefined,
        ocjena: undefined,
        ocjenaTrenutna: undefined,
    });
    const navigate = useNavigate();

    /* otvaranje i zatvaranje prozora za uredivanje tecaja */
    const [popupOpen, setTecajDialogOpen] = useState(false);
    const handleTecajPopupOpen = () => setTecajDialogOpen(true);
    const handleTecajPopupClose = () => setTecajDialogOpen(false);

    const handleLiked = async () => {
        setIsLiked(!isLiked);
        await requests.postWithLoading("tecajevi/favoriti", isLiked);
    }


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
                        paddingTop: "10px",
                        paddingLeft: "10px",
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
                    <Box
                        display={"flex"}
                        flexDirection={"row"}
                        alignItems={"flex-start"}
                        justifyContent={"flex-start"}
                        gap={"20px"}
                    >
                        {tecaj.naslovnaSlikaVersion && (
                            <img
                                style={{
                                    borderRadius: "10px",
                                    objectFit: "cover",
                                    width: "500px",
                                }}
                                src={`${import.meta.env.VITE_REACT_AWS_BUCKET}${
                                    tecaj.naslovnaSlikaVersion
                                }`}
                            />
                        )}
                        <div
                            style={{
                                width: "100%",
                                display: "flex",
                                flexDirection: "column",
                                //justifyContent: "space-between",
                                alignItems: "flex-start",
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
                                    padding: "0 10px",
                                }}
                            >
                                {tecaj.naziv}
                            </Typography>
                        </div>
                    </Box>
                    <div
                        style={{
                            display: "flex",
                            width: "100%",
                            justifyContent: "space-between",
                            alignItems: "center",
                        }}
                    >
                        <div>{"Opis:"}</div>
                        <Box
                            display={"flex"}
                            flexDirection={"row"}
                            alignItems={"center"}
                            sx={{
                                position: "relative",
                            }}
                        >
                            Ukupna ocjena: {tecaj.ocjena} / 5
                            {/* Favorite Icon to the left of Edit Icon */}
                            {globalStore.currentUser !== undefined && (
                                <IconButton
                                    onClick={() => {
                                        setIsLiked(!isLiked);
                                        handleLiked();
                                    }} // Toggle "liked" state
                                    sx={{
                                        backgroundColor: "white",
                                        color: isLiked ? "#f44336" : "grey",
                                        "&:hover": {
                                            backgroundColor: "#fce4ec",
                                        },
                                    }}
                                >
                                    <FavoriteIcon />
                                </IconButton>
                            )}
                            {(globalStore.currentUser?.id === tecaj.vlasnikId ||
                                globalStore.hasPermissions(
                                    LevelPristupa.Moderator
                                )) && (
                                <>
                                    <IconButton
                                        onClick={() => handleTecajPopupOpen()}
                                        sx={{
                                            width: "40px",
                                            height: "40px",
                                        }}
                                    >
                                        <EditIcon />
                                    </IconButton>
                                    <IconButton
                                        onClick={() => setIsPotvrdaOpen(true)}
                                        sx={{
                                            width: "40px",
                                            height: "40px",
                                        }}
                                    >
                                        <DeleteIcon color="primary" />
                                    </IconButton>
                                </>
                            )}
                            {globalStore.currentUser?.id !== tecaj.vlasnikId &&
                                globalStore.currentUser !== undefined && (
                                    <TecajRating
                                        tecajId={tecaj.id}
                                        userRating={tecaj.ocjenaTrenutna}
                                    />
                                )}
                        </Box>
                    </div>
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
