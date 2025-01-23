import { Card, Typography, CardContent, Box, IconButton } from "@mui/material";
import { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import requests from "../../api/agent";
import TeachABitRenderer from "../../components/editor/TeachaBitRenderer";
import NavigateBeforeIcon from "@mui/icons-material/NavigateBefore";
import { ObjavaDto } from "../../models/ObjavaDto";
import UserLink from "../profil/UserLink";
import ObjavaKomentari from "./ObjavaKomentari";
import DeleteIcon from "@mui/icons-material/Delete";
import LikeInfo from "./LikeInfo";
import { useGlobalContext } from "../../context/Global.context";
import EditIcon from "@mui/icons-material/Edit";
import ObjavaEditor from "./ObjavaEditor";
import { LevelPristupa } from "../../enums/LevelPristupa";

export default function ObjavaPage() {
    const [objava, setObjava] = useState<ObjavaDto>({
        sadrzaj: "",
        naziv: "",
    });

    const [isEditing, setIsEditing] = useState(false);

    const { objavaId } = useParams();

    useEffect(() => {
        if (objavaId) {
            getObjavaById(parseInt(objavaId));
        }
    }, [objavaId]);

    const getObjavaById = async (objavaId: number) => {
        const response = await requests.getWithLoading(`objave/${objavaId}`);
        if (response?.data) {
            setObjava(response.data);
        } else {
            navigate("/forum");
        }
    };

    const navigate = useNavigate();
    const globalContext = useGlobalContext();

    const likeObjava = async () => {
        try {
            await requests.postWithLoading(`objave/${objavaId}/like`);
            setObjava((prev: ObjavaDto) => ({
                ...prev,
                likeCount:
                    (prev.likeCount ?? 0) + (prev.liked === false ? 2 : 1),
                liked: true,
            }));
        } catch (exception) {
            console.log(exception);
        }
    };

    const dislikeObjava = async () => {
        await requests.postWithLoading(`objave/${objavaId}/dislike`);
        setObjava((prev: ObjavaDto) => ({
            ...prev,
            likeCount: (prev.likeCount ?? 0) - (prev.liked === true ? 2 : 1),
            liked: false,
        }));
    };

    const clearReaction = async () => {
        await requests.deleteWithLoading(`objave/${objavaId}/reakcija`);
        setObjava((prev: ObjavaDto) => ({
            ...prev,
            likeCount: (prev.likeCount ?? 0) + (prev.liked === true ? -1 : 1),
            liked: undefined,
        }));
    };

    const deleteObjava = async () => {
        const response = await requests.deleteWithLoading(`objave/${objavaId}`);
        if (response && response.message?.severity === "success")
            navigate("/forum");
    };

    const refreshData = async () => {
        if (!objavaId) return;
        const parsedObjavaId = parseInt(objavaId);
        await getObjavaById(parsedObjavaId);
    };

    return (
        <>
            {isEditing && (
                <ObjavaEditor
                    isOpen={isEditing}
                    onClose={() => setIsEditing(false)}
                    refreshData={refreshData}
                    objava={objava}
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
                    sx={{
                        display: "flex",
                        alignItems: "center",
                        margin: "10px",
                    }}
                >
                    <IconButton
                        onClick={() => navigate("/forum")}
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
                            justifyContent: "space-between",
                            alignItems: "center",
                        }}
                    >
                        <Typography
                            color="primary"
                            variant="h5"
                            component="div"
                            sx={{
                                overflow: "hidden",
                                whiteSpace: "break-spaces",
                                maxWidth: "95%",
                                color: "black",
                            }}
                        >
                            {objava.naziv}
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
                                    id: objava.vlasnikId,
                                    username: objava.vlasnikUsername,
                                    profilnaSlikaVersion:
                                        objava.vlasnikProfilnaSlikaVersion,
                                }}
                            />
                        </Box>
                    </div>
                    <TeachABitRenderer content={objava.sadrzaj} />
                    <Box
                        display={"flex"}
                        flexDirection={"row"}
                        justifyContent={"flex-end"}
                        alignItems={"center"}
                        gap="10px"
                    >
                        {globalContext.currentUser?.id === objava.vlasnikId && (
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
                        {(globalContext.currentUser?.id === objava.vlasnikId ||
                            globalContext.hasPermissions(
                                LevelPristupa.Moderator
                            )) && (
                            <>
                                <IconButton
                                    onClick={() => deleteObjava()}
                                    sx={{
                                        width: "40px",
                                        height: "40px",
                                    }}
                                    id="objavaPage-deleteButton"
                                >
                                    <DeleteIcon color="primary"></DeleteIcon>
                                </IconButton>
                            </>
                        )}
                        <LikeInfo
                            likeCount={objava.likeCount}
                            onClear={clearReaction}
                            onDislike={dislikeObjava}
                            onLike={likeObjava}
                            liked={objava.liked}
                        />
                    </Box>
                    {objava.id && objava.vlasnikId && (
                        <ObjavaKomentari
                            objavaId={objava.id}
                            vlasnikId={objava.vlasnikId}
                        />
                    )}
                </CardContent>
            </Card>
        </>
    );
}
