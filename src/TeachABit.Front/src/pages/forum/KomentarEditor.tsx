import {
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
} from "@mui/material";
import { useMemo, useState } from "react";
import { KomentarDto } from "../../models/KomentarDto";
import TeachABitEditor from "../../components/editor/TeachABitTextEditor";
import { useParams } from "react-router-dom";
import requests from "../../api/agent";
import { UpdateKomentarDto } from "../../models/UpdateKomentarDto";

interface Props {
    refreshData: () => Promise<any>;
    onClose: () => void;
    isOpen: boolean;
    nadKomentarId?: number | undefined;
    komentar?: KomentarDto;
}

export default function KomentarEditor(props: Props) {
    const [komentar, setKomentar] = useState<KomentarDto>({
        sadrzaj: props.komentar?.sadrzaj ?? "",
    });

    const { objavaId } = useParams();

    const parsedObjavaId = useMemo(() => {
        if (!objavaId) return undefined;
        return parseInt(objavaId);
    }, [objavaId]);

    const handleStvoriKomentar = async () => {
        if (!parsedObjavaId) return;
        if (props.nadKomentarId) komentar.nadKomentarId = props.nadKomentarId;
        const response = await requests.postWithLoading(
            `objave/${parsedObjavaId}/komentari`,
            komentar
        );
        if (response && response.data) {
            handleClose(true);
        }
    };

    const handleUpdateKomentar = async () => {
        if (!parsedObjavaId || !props.komentar) return;
        const updateKomentar: UpdateKomentarDto = {
            sadrzaj: komentar.sadrzaj,
            id: props.komentar.id,
        };
        const response = await requests.putWithLoading(
            `objave/komentari`,
            updateKomentar
        );
        if (response?.data) {
            handleClose(true);
        }
    };

    const handleClose = (reload: boolean = false) => {
        setKomentar({
            sadrzaj: "",
            objavaId: undefined,
        });
        props.onClose();
        if (reload) props.refreshData();
    };

    return (
        <>
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
                            {`Komentar`}
                        </div>
                    </div>
                </DialogTitle>
                <DialogContent
                    sx={{
                        width: "100%",
                        display: "flex",
                        flexDirection: "column",
                        gap: "20px",
                        paddingTop: "10px !important",
                        maxHeight: 300,
                        scrollbarGutter: "stable",
                    }}
                >
                    <TeachABitEditor
                        content={komentar.sadrzaj}
                        onUpdate={(value: string) =>
                            setKomentar((prev: any) => ({
                                ...prev,
                                sadrzaj: value,
                            }))
                        }
                    />
                </DialogContent>
                <DialogActions>
                    <Button variant="outlined" onClick={() => handleClose()}>
                        Odustani
                    </Button>
                    <Button
                        variant="contained"
                        onClick={() => {
                            if (!props.komentar?.id) handleStvoriKomentar();
                            else handleUpdateKomentar();
                        }}
                    >
                        {!props.komentar?.id
                            ? "Stvori komentar"
                            : "Ažuriraj komentar"}
                    </Button>
                </DialogActions>
            </Dialog>
        </>
    );
}
