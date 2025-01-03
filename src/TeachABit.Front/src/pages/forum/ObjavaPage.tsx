import {
    Card,
    Breadcrumbs,
    Typography,
    CardContent,
    Link,
} from "@mui/material";
import { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import requests from "../../api/agent";
import TeachABitRenderer from "../../components/editor/TeachaBitRenderer";
import { ObjavaDto } from "../../models/ObjavaDto";
import UserLink from "../profil/UserLink";
import ObjavaKomentari from "./ObjavaKomentari";
import LikeInfo from "./LikeInfo";

export default function ObjavaPage() {
    const [objava, setObjava] = useState<ObjavaDto>({
        sadrzaj: "",
        naziv: "",
    });

    const { objavaId } = useParams();

    useEffect(() => {
        if (objavaId) {
            getObjavaById(parseInt(objavaId));
        }
    }, [objavaId]);

    const getObjavaById = async (objavaId: number) => {
        const response = await requests.getWithLoading(`objave/${objavaId}`);
        if (response.data) {
            setObjava(response.data);
        }
    };

    const navigate = useNavigate();

    const likeObjava = async () => {
        await requests.postWithLoading(`objave/${objavaId}/like`);
        setObjava((prev: ObjavaDto) => ({
            ...prev,
            likeCount: (prev.likeCount ?? 0) + (prev.liked === false ? 2 : 1),
            liked: true,
        }));
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
                        onClick={() => navigate("/forum")}
                    >
                        Objave
                    </Link>

                    <Typography sx={{ color: "text.primary" }}>
                        {objava.id}
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
                            {objava.naziv}
                        </Typography>
                        <UserLink
                            user={{
                                id: objava.vlasnikId,
                                username: objava.vlasnikUsername,
                                profilnaSlikaVersion:
                                    objava.vlasnikProfilnaSlikaVersion,
                            }}
                        />
                    </div>
                    <TeachABitRenderer content={objava.sadrzaj} />
                    <LikeInfo
                        likeCount={objava.likeCount}
                        onClear={clearReaction}
                        onDislike={dislikeObjava}
                        onLike={likeObjava}
                        liked={objava.liked}
                    />
                    {objava.id && <ObjavaKomentari objavaId={objava.id} />}
                </CardContent>
            </Card>
        </>
    );
}
