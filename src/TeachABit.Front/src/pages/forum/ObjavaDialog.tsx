import {
    Dialog,
    DialogTitle,
    DialogContent,
    TextField,
    DialogActions,
    Button,
    Avatar,
    Typography,
} from "@mui/material";
import { ChangeEvent, useEffect, useMemo, useState } from "react";
import { DetailedObjavaDto, ObjavaDto } from "../../models/ObjavaDto";
import requests from "../../api/agent";
import { KomentarDto } from "../../models/KomentarDto";
import { formatDistanceToNow } from "date-fns";
import TeachABitEditor from "../../components/editor/TeachABitTextEditor";
import TeachABitRenderer from "../../components/editor/TeachaBitRenderer";
import UserLink from "../profil/UserLink";

interface Props {
    objavaId?: number;
    refreshData: () => Promise<any>;
    onClose: () => void;
    isOpen: boolean;
}

export default function ObjavaDialog(props: Props) {
    const [objava, setObjava] = useState<DetailedObjavaDto>({
        sadrzaj: "",
        naziv: "",
    });

    useEffect(() => {
        if (props.objavaId) {
            getObjavaById(props.objavaId);
        }
    }, [props.objavaId]);

    const getObjavaById = async (objavaId: number) => {
        const response = await requests.getWithLoading(`objave/${objavaId}`);
        if (response.data) {
            setObjava(response.data);
        }
    };

    const handleClose = (reload: boolean = false) => {
        setObjava({
            naziv: "",
            sadrzaj: "",
        });
        props.onClose();
        if (reload) props.refreshData();
    };

    const handleStvoriObjavu = async (objava: ObjavaDto) => {
        const response = await requests.postWithLoading("objave", objava);
        if (response.data) {
            handleClose(true);
        }
    };

    const isCreating = useMemo(() => {
        return !props.objavaId;
    }, [props.objavaId]);

    return (
        <>
            {props.isOpen && (
                <Dialog
                    open={props.isOpen}
                    onClose={() => {
                        handleClose();
                    }}
                    maxWidth={"md"}
                >
                    <DialogTitle sx={{ maxWidth: "100%" }}>
                        <div
                            style={{
                                display: "flex",
                                flexDirection: "row",
                                justifyContent: "space-between",
                                alignItems: "flex-start",
                                width: "100%",
                            }}
                        >
                            <div
                                style={{
                                    overflowX: "hidden",
                                    whiteSpace: "normal",
                                    maxWidth: "80%",
                                }}
                            >
                                {isCreating ? `Nova objava` : `${objava.naziv}`}
                            </div>
                            <UserLink
                                userId={objava.vlasnikId}
                                username={objava.vlasnikUsername}
                            />
                        </div>
                    </DialogTitle>
                    <DialogContent
                        sx={{
                            height: isCreating ? 600 : "auto",
                            display: "flex",
                            flexDirection: "column",
                            gap: "20px",
                            paddingTop: "10px !important",
                            maxHeight: "70vh",
                            minWidth: "40vw",
                        }}
                    >
                        {isCreating && (
                            <TextField
                                fullWidth
                                autoFocus
                                label="Naziv"
                                variant="outlined"
                                value={objava.naziv || ""}
                                onChange={(e: ChangeEvent<HTMLInputElement>) =>
                                    setObjava((prev: any) => ({
                                        ...prev,
                                        naziv: e.target.value,
                                    }))
                                }
                            />
                        )}
                        {isCreating ? (
                            <TeachABitEditor
                                content={objava.sadrzaj}
                                onUpdate={(value: string) =>
                                    setObjava((prev: any) => ({
                                        ...prev,
                                        sadrzaj: value,
                                    }))
                                }
                            />
                        ) : (
                            <TeachABitRenderer content={objava.sadrzaj} />
                        )}
                        {!isCreating && (
                            <div>
                                {"Komentari:"}
                                <hr style={{ width: "100%" }} />
                            </div>
                        )}

                        {!isCreating && objava.komentari && (
                            <div
                                style={{
                                    display: "flex",
                                    flexDirection: "column",
                                    gap: "20px",
                                }}
                            >
                                {objava.komentari.map(
                                    (komentar: KomentarDto) => (
                                        <div
                                            style={{
                                                display: "flex",
                                                flexDirection: "row",
                                                gap: "10px",
                                                alignItems: "center",
                                            }}
                                        >
                                            <Avatar>
                                                {komentar.vlasnikUsername
                                                    ? komentar
                                                          .vlasnikUsername[0]
                                                    : ""}
                                            </Avatar>
                                            <div>
                                                {komentar.sadrzaj}

                                                <p
                                                    style={{
                                                        margin: 0,
                                                        color: "gray",
                                                        fontSize: 15,
                                                    }}
                                                >
                                                    {`${formatDistanceToNow(
                                                        new Date(
                                                            komentar.createdDateTime
                                                        ),
                                                        { addSuffix: true }
                                                    )} by ${
                                                        komentar.vlasnikUsername
                                                    }`}
                                                </p>
                                            </div>
                                        </div>
                                    )
                                )}
                            </div>
                        )}
                    </DialogContent>
                    <DialogActions>
                        {!objava.id && (
                            <Button
                                variant="contained"
                                onClick={() => handleStvoriObjavu(objava)}
                            >
                                Stvori objavu
                            </Button>
                        )}
                    </DialogActions>
                </Dialog>
            )}
        </>
    );
}
