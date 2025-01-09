import { IconButton } from "@mui/material";
import { formatDistanceToNow } from "date-fns";
import TeachABitRenderer from "../../components/editor/TeachaBitRenderer";
import { KomentarDto } from "../../models/KomentarDto";
import UserLink from "../profil/UserLink";
import ReplyIcon from "@mui/icons-material/Reply";
import CreateKomentar from "./CreateKomentar";
import { Dispatch, SetStateAction, useMemo, useState } from "react";
import LikeInfo from "./LikeInfo";
import requests from "../../api/agent";
import { useGlobalContext } from "../../context/Global.context";

interface Props {
    komentar: KomentarDto;
    refreshData: () => Promise<any>;
    selectedNadKomentarId?: number | undefined;
    setSelectedNadKomentarId: Dispatch<SetStateAction<number | undefined>>;
    level?: number | undefined;
    collapsedComments: Record<number, boolean>;
    toggleCollapse: (komentarId: number | undefined) => void;
}

export default function Komentar(props: Props) {
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

    const globalContext = useGlobalContext();

    const likeKomentar = async () => {
        await requests.postWithLoading(
            `objave/komentari/${props.komentar.id}/like`
        );
        const wasDisliked = liked === false;
        setLikeCount((prev) => (prev ?? 0) + (wasDisliked ? 2 : 1));
        setLiked(true);
    };

    const dislikeKomentar = async () => {
        await requests.postWithLoading(
            `objave/komentari/${props.komentar.id}/dislike`
        );
        const wasLiked = liked === true;
        setLikeCount((prev) => (prev ?? 0) - (wasLiked ? 2 : 1));
        setLiked(false);
    };

    const clearReaction = async () => {
        await requests.deleteWithLoading(
            `objave/komentari/${props.komentar.id}/reakcija`
        );
        const shouldLower = liked === true;
        setLikeCount((prev) => (prev ?? 0) + (shouldLower ? -1 : +1));
        setLiked(undefined);
    };

    return (
        <>
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
                        backgroundColor: isHidden ? "#922728" : "lightgray",
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
                                    <p
                                        style={{
                                            margin: 0,
                                            color: "gray",
                                            fontSize: 14,
                                            userSelect: "none",
                                            cursor: "default",
                                        }}
                                    >
                                        {`${formatDistanceToNow(
                                            new Date(
                                                props.komentar.createdDateTime
                                            ),
                                            { addSuffix: true }
                                        )} by`}
                                    </p>
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
                                <LikeInfo
                                    likeCount={likeCount}
                                    onClear={clearReaction}
                                    onDislike={dislikeKomentar}
                                    onLike={likeKomentar}
                                    liked={liked}
                                    size="small"
                                />
                                {globalContext.userIsLoggedIn && (
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
            <CreateKomentar
                refreshData={() => props.refreshData()}
                isOpen={props.selectedNadKomentarId === props.komentar.id}
                onClose={() => props.setSelectedNadKomentarId(undefined)}
                nadKomentarId={props.selectedNadKomentarId}
            />
        </>
    );
}
