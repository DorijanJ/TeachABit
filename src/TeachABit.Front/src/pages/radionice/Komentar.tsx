import { IconButton } from "@mui/material";
import { formatDistanceToNow } from "date-fns";
import TeachABitRenderer from "../../components/editor/TeachaBitRenderer";
import { RadionicaKomentarDto } from "../../models/RadionicaKomentarDto";
import UserLink from "../profil/UserLink";
import ReplyIcon from "@mui/icons-material/Reply";
import { Dispatch, SetStateAction, useMemo, useState } from "react";
import LikeInfo from "./LikeInfo";
import requests from "../../api/agent";
import KomentarEditor from "./KomentarEditor";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import { hr } from "date-fns/locale";
import { LevelPristupa } from "../../enums/LevelPristupa";
import { observer } from "mobx-react";
import globalStore from "../../stores/GlobalStore";

interface Props {
    komentar: RadionicaKomentarDto;
    refreshData: () => Promise<any>;
    selectedNadKomentarId?: number | undefined;
    setSelectedNadKomentarId: Dispatch<SetStateAction<number | undefined>>;
    level?: number | undefined;
    collapsedComments: Record<number, boolean>;
    toggleCollapse: (komentarId: number | undefined) => void;
}

export const Komentar = (props: Props) => {
    const isHidden = useMemo(() => {
        return (
            props.komentar.id !== undefined &&
            props.collapsedComments[props.komentar.id]
        );
    }, [props.collapsedComments, props.komentar.id]);

    const [likeCount, setLikeCount] = useState<number | undefined>(
        props.komentar.likeCount
    );
    const [liked, setLiked] = useState<boolean | undefined>(
        props.komentar.liked
    );
    const [isEditing, setIsEditing] = useState(false);

    const likeKomentar = async () => {
        await requests.postWithLoading(
            `radionice/komentari/${props.komentar.id}/like`
        );
        const wasDisliked = liked === false;
        setLikeCount((prev) => (prev ?? 0) + (wasDisliked ? 2 : 1));
        setLiked(true);
    };

    const dislikeKomentar = async () => {
        await requests.postWithLoading(
            `radionice/komentari/${props.komentar.id}/dislike`
        );
        const wasLiked = liked === true;
        setLikeCount((prev) => (prev ?? 0) - (wasLiked ? 2 : 1));
        setLiked(false);
    };

    const clearReaction = async () => {
        await requests.deleteWithLoading(
            `radionice/komentari/${props.komentar.id}/reakcija`
        );
        const shouldLower = liked === true;
        setLikeCount((prev) => (prev ?? 0) + (shouldLower ? -1 : +1));
        setLiked(undefined);
    };

    const deleteKomentar = async () => {
        if (!props.komentar.id) return;
        const response = await requests.deleteWithLoading(
            `radionice/komentari/${props.komentar.id}`
        );
        if (response?.message?.severity === "success") props.refreshData();
    };

    return (
        <>
            {isEditing && (
                <KomentarEditor
                    isOpen
                    onClose={() => setIsEditing(false)}
                    refreshData={props.refreshData}
                    komentar={props.komentar}
                />
            )}
            <div
                style={{
                    display: "flex",
                    flexDirection: "row",
                    width: "100%",
                    justifyContent: "flex-start",
                    marginTop: "4px",
                }}
            >
                <div
                    style={{
                        visibility:
                            props.komentar.podKomentarList !== undefined &&
                            props.komentar.podKomentarList.length > 0
                                ? "visible"
                                : "hidden",
                        backgroundColor: isHidden ? "#3a7ca5" : "lightgray",
                        width: "8px",
                        padding: "0 5px",
                        borderRadius: "5px",
                        marginLeft: (props.level ?? 0) * 30,
                        cursor: "pointer",
                    }}
                    onClick={() => {
                        const selection = window.getSelection();
                        if (selection) {
                            selection.removeAllRanges();
                        }
                        props.toggleCollapse(props.komentar.id);
                    }}
                ></div>
                <div
                    style={{
                        display: "flex",
                        flexDirection: "row",
                        alignItems: "flex-start",
                        width: "100%",
                        padding: "10px",
                        gap: "10px",
                    }}
                >
                    <div
                        style={{
                            display: "flex",
                            flexDirection: "column",
                            gap: "5px",
                            width: "100%",
                        }}
                    >
                        <TeachABitRenderer content={props.komentar.sadrzaj} />

                        <div
                            style={{
                                display: "flex",
                                flexDirection: "row",
                                width: "100%",

                                justifyContent: "space-between",
                            }}
                        >
                            {props.komentar.createdDateTime && (
                                <div
                                    style={{
                                        display: "flex",
                                        flexDirection: "row",
                                        gap: "5px",
                                        alignItems: "center",
                                    }}
                                >
                                    <UserLink
                                        user={{
                                            id: props.komentar.vlasnikId,
                                            username:
                                                props.komentar.vlasnikUsername,
                                            profilnaSlikaVersion:
                                                props.komentar
                                                    .vlasnikProfilnaSlikaVersion,
                                        }}
                                    />
                                    <p
                                        style={{
                                            margin: 0,
                                            color: "gray",
                                            fontSize: 14,
                                            userSelect: "none",
                                            cursor: "default",
                                        }}
                                    >
                                        {!props.komentar.lastUpdatedDateTime
                                            ? `Komentirano ${formatDistanceToNow(
                                                  new Date(
                                                      props.komentar.createdDateTime
                                                  ),
                                                  {
                                                      addSuffix: true,
                                                      locale: hr,
                                                  }
                                              )}`
                                            : `Izmijenjeno ${formatDistanceToNow(
                                                  new Date(
                                                      props.komentar.lastUpdatedDateTime
                                                  ),
                                                  {
                                                      addSuffix: true,
                                                      locale: hr,
                                                  }
                                              )}`}
                                        <br></br>
                                    </p>
                                </div>
                            )}
                            <div
                                style={{
                                    display: "flex",
                                    flexDirection: "row",
                                    alignItems: "center",
                                    gap: "10px",
                                }}
                            >
                                {!props.komentar.isDeleted && (
                                    <>
                                        {globalStore.currentUser?.id ===
                                            props.komentar.vlasnikId && (
                                            <IconButton
                                                sx={{
                                                    width: "30px",
                                                    height: "30px",
                                                }}
                                                onClick={() => {
                                                    setIsEditing(true);
                                                }}
                                            >
                                                <EditIcon
                                                    color="primary"
                                                    fontSize="small"
                                                />
                                            </IconButton>
                                        )}
                                        {(globalStore.currentUser?.id ===
                                            props.komentar.vlasnikId ||
                                            globalStore.hasPermissions(
                                                LevelPristupa.Moderator
                                            )) && (
                                            <>
                                                <IconButton
                                                    sx={{
                                                        width: "30px",
                                                        height: "30px",
                                                    }}
                                                    onClick={() => {
                                                        deleteKomentar();
                                                    }}
                                                >
                                                    <DeleteIcon
                                                        color="primary"
                                                        fontSize="small"
                                                    />
                                                </IconButton>
                                            </>
                                        )}
                                    </>
                                )}
                                <LikeInfo
                                    likeCount={likeCount}
                                    onClear={clearReaction}
                                    onDislike={dislikeKomentar}
                                    onLike={likeKomentar}
                                    liked={liked}
                                    size="small"
                                />
                                {globalStore.currentUser !== undefined && (
                                    <IconButton
                                        sx={{ width: "30px", height: "30px" }}
                                        onClick={() => {
                                            props.setSelectedNadKomentarId(
                                                props.komentar.id
                                            );
                                        }}
                                    >
                                        <ReplyIcon
                                            color="primary"
                                            fontSize="small"
                                        />
                                    </IconButton>
                                )}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <KomentarEditor
                refreshData={() => props.refreshData()}
                isOpen={props.selectedNadKomentarId === props.komentar.id}
                onClose={() => props.setSelectedNadKomentarId(undefined)}
                nadKomentarId={props.selectedNadKomentarId}
            />
        </>
    );
};

export default observer(Komentar);
