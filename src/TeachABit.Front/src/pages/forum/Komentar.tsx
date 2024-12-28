import { Button, IconButton } from "@mui/material";
import { formatDistanceToNow } from "date-fns";
import TeachABitRenderer from "../../components/editor/TeachaBitRenderer";
import { KomentarDto } from "../../models/KomentarDto";
import UserLink from "../profil/UserLink";
import ThumbUpIcon from "@mui/icons-material/ThumbUp";
import ReplyIcon from "@mui/icons-material/Reply";
import ThumbDownIcon from "@mui/icons-material/ThumbDown";
import CreateKomentar from "./CreateKomentar";
import { Dispatch, SetStateAction, useMemo } from "react";

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
                        backgroundColor:
                            isHidden &&
                            props.komentar.podKomentarList !== undefined &&
                            props.komentar.podKomentarList.length > 0
                                ? "#922728"
                                : "lightgray",
                        width: "8px",
                        padding: "0 5px",
                        borderRadius: "5px",
                        marginLeft: (props.level ?? 0) * 30,
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
                                    gap: "10px",
                                }}
                            >
                                <IconButton
                                    sx={{ width: "30px", height: "30px" }}
                                >
                                    <ThumbUpIcon
                                        color="primary"
                                        fontSize="small"
                                    />
                                </IconButton>
                                <IconButton
                                    sx={{ width: "30px", height: "30px" }}
                                >
                                    <ThumbDownIcon
                                        color="primary"
                                        fontSize="small"
                                    />
                                </IconButton>
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
