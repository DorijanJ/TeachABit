import {
    Card,
    Typography,
    CardContent,
    Box,
    IconButton,
    Rating,
} from "@mui/material";
import { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import requests from "../../api/agent";
import TeachABitRenderer from "../../components/editor/TeachaBitRenderer";
import UserLink from "../profil/UserLink";
import DeleteIcon from "@mui/icons-material/Delete";
import EditIcon from "@mui/icons-material/Edit";
import RadionicaEditor from "./RadionicaEditor";
import NavigateBeforeIcon from "@mui/icons-material/NavigateBefore";
import { RadionicaDto } from "../../models/RadionicaDto";
import RadionicaKomentari from "./RadionicaKomentari";
import PotvrdiPopup from "../../components/dialogs/PotvrdiPopup";
import { LevelPristupa } from "../../enums/LevelPristupa";
import globalStore from "../../stores/GlobalStore";
import { observer } from "mobx-react";
import React from "react";
import FavoriteIcon from "@mui/icons-material/Favorite";

export const RadionicaPage = () => {
    const [value, setValue] = React.useState<number | null>(2);
    const [radionica, setRadionica] = useState<RadionicaDto>({
        naziv: "",
        opis: "",
        cijena: 0,
        vrijemeRadionice: undefined,
    });

    const [isEditing, setIsEditing] = useState(false);

    const { radionicaId } = useParams();

    useEffect(() => {
        if (radionicaId) {
            getRadionicaById(parseInt(radionicaId));
        }
    }, [radionicaId]);

    const getRadionicaById = async (radionicaId: number) => {
        const response = await requests.getWithLoading(
            `radionice/${radionicaId}`
        );
        if (response?.data) {
            setRadionica(response.data);
        } else {
            navigate("/radionice");
        }
    };

    const navigate = useNavigate();

    /*const likeRadionica = async () => {
        try {
            await requests.postWithLoading(`radionice/${radionicaId}/like`);
            setRadionica((prev: RadionicaDto) => ({
                ...prev,
                likeCount:
                    (prev.likeCount ?? 0) + (prev.liked === false ? 2 : 1),
                liked: true,
            }));
        } catch (exception) {
            console.log(exception);
        }
    };*/

    /*const dislikeRadionica = async () => {
        await requests.postWithLoading(`radionice/${radionicaId}/dislike`);
        setRadionica((prev: RadionicaDto) => ({
            ...prev,
            likeCount: (prev.likeCount ?? 0) - (prev.liked === true ? 2 : 1),
            liked: false,
        }));
    };

    const clearReaction = async () => {
        await requests.deleteWithLoading(`radionice/${radionicaId}/reakcija`);
        setRadionica((prev: RadionicaDto) => ({
            ...prev,
            likeCount: (prev.likeCount ?? 0) + (prev.liked === true ? -1 : 1),
            liked: undefined,
        }));
    };*/

    const [isPotvrdaOpen, setIsPotvrdaOpen] = useState(false);

    const deleteRadionica = async () => {
        const response = await requests.deleteWithLoading(
            `radionice/${radionicaId}`
        );
        if (response && response.message?.severity === "success") navigate(-1);
    };

    const refreshData = async () => {
        if (!radionicaId) return;
        const parsedRadionicaId = parseInt(radionicaId);
        await getRadionicaById(parsedRadionicaId);
    };

    const [isLiked, setIsLiked] = useState(false);
    const handleLiked = async () => {
        const newIsLiked = !isLiked; // Toggle the liked state


        await requests.postWithLoading(
            `radionice/${radionicaId}/favoriti`,
            {isLiked: newIsLiked} // Send the new liked state in the body
        );

        // Update the state only if the request is successful
        setIsLiked(newIsLiked);
        setRadionica((prev) => ({
            ...prev,
            favorit: newIsLiked, // Update favorit field in the state
        }));

    };

    return (
        <>
            {isPotvrdaOpen && (
                <PotvrdiPopup
                    onConfirm={() => deleteRadionica()}
                    onClose={() => setIsPotvrdaOpen(false)}
                    tekstPitanje="Jeste li sigurni da želite izbrisati radionicu?"
                    tekstOdgovor="Izbriši"
                />
            )}

            {isEditing && (
                <RadionicaEditor
                    isOpen={isEditing}
                    onClose={() => setIsEditing(false)}
                    refreshData={refreshData}
                    radionica={radionica}
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
                            id: radionica.vlasnikId,
                            username: radionica.vlasnikUsername,
                            profilnaSlikaVersion:
                                radionica.vlasnikProfilnaSlikaVersion,
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
                        {radionica.naslovnaSlikaVersion && (
                            <img
                                style={{
                                    borderRadius: "10px",
                                    objectFit: "cover",
                                    width: "500px",
                                }}
                                src={`${import.meta.env.VITE_REACT_AWS_BUCKET}${
                                    radionica.naslovnaSlikaVersion
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
                                {radionica.naziv}
                            </Typography>
                        </div>
                    </Box>
                    <div
                        style={{
                            width: "100%",
                            display: "flex",
                            justifyContent: "space-between",
                            alignItems: "center",
                        }}
                    ></div>

                    <div>{"Opis:"}</div>
                    <TeachABitRenderer content={radionica.opis ?? ""} />
                    <Box
                        className="ocjena-edit-delete-wrapper"
                        display={"flex"}
                        flexDirection={"row"}
                        justifyContent={"space-between"}
                        alignItems={"center"}
                        gap="10px"
                    >
                        {/*globalContext.currentUser?.id === radionica.vlasnikId*/}
                        {globalStore.currentUser?.id ===
                            radionica.vlasnikId && (
                            <Box
                                display={"flex"}
                                flexDirection={"row"}
                                justifySelf={"start"}
                                alignItems="center"
                                gap="10px"
                                sx={{ "& > legend": { mt: 2 } }}
                            >
                                {<Typography>Ocijeni radionicu: </Typography>}
                                <Rating
                                    //title="Ocijeni radionicu: "
                                    name="simple-controlled"
                                    value={value}
                                    onChange={(event, newValue) => {
                                        setValue(newValue);
                                    }}
                                />
                            </Box>
                        )}

                        <Box
                            display={"flex"}
                            flexDirection={"row"}
                            justifyContent={"flex-end"}
                            alignItems={"center"}
                            gap="10px"
                        >
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
                            {globalStore.currentUser?.id ===
                                radionica.vlasnikId && (
                                <IconButton
                                    onClick={() => setIsEditing(true)}
                                    sx={{
                                        width: "40px",
                                        height: "40px",
                                    }}
                                >
                                    <EditIcon color="primary"></EditIcon>
                                </IconButton>
                            )}
                            {(globalStore.currentUser?.id ===
                                radionica.vlasnikId ||
                                globalStore.hasPermissions(
                                    LevelPristupa.Moderator
                                )) && (
                                <>
                                    <IconButton
                                        onClick={() => setIsPotvrdaOpen(true)}
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
                    </Box>
                    {radionica.id && (
                        <RadionicaKomentari radionicaId={radionica.id} />
                    )}
                </CardContent>
            </Card>
        </>
    );
};
export default observer(RadionicaPage);
