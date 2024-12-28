import { IconButton } from "@mui/material";
import { formatDistanceToNow } from "date-fns";
import TeachABitRenderer from "../../components/editor/TeachaBitRenderer";
import { KomentarDto } from "../../models/KomentarDto";
import UserLink from "../profil/UserLink";
import ThumbUpIcon from "@mui/icons-material/ThumbUp";
import ReplyIcon from "@mui/icons-material/Reply";
import ThumbDownIcon from "@mui/icons-material/ThumbDown";
import CreateKomentar from "./CreateKomentar";
import { Dispatch, SetStateAction } from "react";

interface Props {
    komentar: KomentarDto;
    refreshData: () => Promise<any>;
    selectedNadKomentarId?: number | undefined;
    setSelectedNadKomentarId: Dispatch<SetStateAction<number | undefined>>;
    level?: number | undefined;
}

export default function Komentar(props: Props) {
    return (
        <>
            <div
                style={{
                    display: "flex",
                    flexDirection: "row",
                    gap: "20px",
                    alignItems: "flex-start",
                    paddingTop: "10px",
                    paddingRight: "0px",
                    paddingBottom: "0px",
                    paddingLeft: (props.level ?? 0) * 30,
                    width: "100%",
                }}
            >
                <UserLink
                    user={{
                        id: props.komentar.vlasnikId,
                        username: props.komentar.vlasnikUsername,
                        profilnaSlikaVersion:
                            props.komentar.vlasnikProfilnaSlikaVersion,
                    }}
                />
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
                            <p
                                style={{
                                    margin: 0,
                                    color: "gray",
                                    fontSize: 14,
                                }}
                            >
                                {`${formatDistanceToNow(
                                    new Date(props.komentar.createdDateTime),
                                    { addSuffix: true }
                                )} by ${props.komentar.vlasnikUsername}`}
                            </p>
                        )}
                        <div
                            style={{
                                display: "flex",
                                flexDirection: "row",
                                gap: "10px",
                            }}
                        >
                            <IconButton sx={{ width: "30px", height: "30px" }}>
                                <ThumbUpIcon color="primary" fontSize="small" />
                            </IconButton>
                            <IconButton sx={{ width: "30px", height: "30px" }}>
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
                                <ReplyIcon color="primary" fontSize="small" />
                            </IconButton>
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
