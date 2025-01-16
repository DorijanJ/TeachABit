import {
    Card,
    Breadcrumbs,
    Typography,
    CardContent,
    Link,
    Box,
    IconButton,
} from "@mui/material";
import { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import requests from "../../api/agent";
import TeachABitRenderer from "../../components/editor/TeachaBitRenderer";
import UserLink from "../profil/UserLink";
import DeleteIcon from "@mui/icons-material/Delete";
import { useGlobalContext } from "../../context/Global.context";
import EditIcon from "@mui/icons-material/Edit";
import RadionicaEditor from "./RadionicaEditor";
import { RadionicaDto } from "../../models/RadionicaDto";

export default function RadionicaPage() {
    const [radionica, setRadionica] = useState<RadionicaDto>({
        //sadrzaj: "",
        naziv: "naslov123",
        opis: "tema123"
    });

    const [isEditing, setIsEditing] = useState(false);

    const { radionicaId } = useParams();

    useEffect(() => {
        if (radionicaId) {
            getRadionicaById(parseInt(radionicaId));
        }
    }, [radionicaId]);

    const getRadionicaById = async (radionicaId: number) => {
        const response = await requests.getWithLoading(`radionice/${radionicaId}`);
        if (response?.data) {
            setRadionica(response.data);
        } else {
            navigate("/radionice");
        }
    };

    const navigate = useNavigate();
    const globalContext = useGlobalContext();

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

    const deleteRadionica = async () => {
        const response = await requests.deleteWithLoading(`radionice/${radionicaId}`);
        if (response && response.message?.severity === "success")
            navigate("/radionice");
    };

    const refreshData = async () => {
        if (!radionicaId) return;
        const parsedRadionicaId = parseInt(radionicaId);
        await getRadionicaById(parsedRadionicaId);
    };

    return (
        <>
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
                <Breadcrumbs aria-label="breadcrumb" sx={{ padding: "15px" }}>
                    <Link
                        underline="hover"
                        color="inherit"
                        onClick={() => navigate("/radionice")}
                    >
                        Objave
                    </Link>

                    <Typography sx={{ color: "text.primary" }}>
                        {radionica.id}
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
                            {radionica.naziv}
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
                                    id: radionica.vlasnikId,
                                    username: radionica.vlasnikUsername,
                                    profilnaSlikaVersion:
                                        radionica.vlasnikProfilnaSlikaVersion,
                                }}
                            />
                        </Box>
                    </div>
                    <TeachABitRenderer content={radionica.opis ?? ""} />
                    <Box
                        display={"flex"}
                        flexDirection={"row"}
                        justifyContent={"flex-end"}
                        alignItems={"center"}
                        gap="10px"
                    >
                        {globalContext.currentUser?.id === radionica.vlasnikId && (
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
                        {(globalContext.currentUser?.id === radionica.vlasnikId ||
                            globalContext.isAdmin) && (
                                <>
                                    <IconButton
                                        onClick={() => deleteRadionica()}
                                        sx={{
                                            width: "40px",
                                            height: "40px",
                                        }}
                                    >
                                        <DeleteIcon color="primary"></DeleteIcon>
                                    </IconButton>
                                </>
                            )}
                        {/*<LikeInfo
                            likeCount={radionica.likeCount}
                            onClear={clearReaction}
                            onDislike={dislikeRadionica}
                            onLike={likeRadionica}
                            liked={radionica.liked}
                        />*/}
                    </Box>
                    {/*radionica.id && <RadionicaKomentari radionicaId={radionica.id} />*/}
                </CardContent>
            </Card>
        </>
    );
}
