import {
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
} from "@mui/material";
import { useMemo, useState } from "react";
import { RadionicaKomentarDto } from "../../models/RadionicaKomentarDto";
import TeachABitEditor from "../../components/editor/TeachABitTextEditor";
import { useParams } from "react-router-dom";
import requests from "../../api/agent";
import { UpdateKomentarDto } from "../../models/UpdateKomentarDto";

interface Props {
    refreshData: () => Promise<any>;
    onClose: () => void;
    isOpen: boolean;
    nadKomentarId?: number | undefined;
    komentar?: RadionicaKomentarDto;
}

export default function KomentarEditor(props: Props) {
    const [komentar, setKomentar] = useState<RadionicaKomentarDto>({
        sadrzaj: props.komentar?.sadrzaj ?? "",
    });

    const { radionicaId } = useParams();

    const parsedRadionicaId = useMemo(() => {
        if (!radionicaId) return undefined;
        return parseInt(radionicaId);
    }, [radionicaId]);

    const handleStvoriKomentar = async () => {
        if (!parsedRadionicaId) return;
        if (props.nadKomentarId) komentar.nadKomentarId = props.nadKomentarId;
        const response = await requests.postWithLoading(
            `radionice/${parsedRadionicaId}/komentari`,
            komentar
        );
        if (response && response.data) {
            handleClose(true);
        }
    };

    const handleUpdateKomentar = async () => {
        if (!parsedRadionicaId || !props.komentar) return;
        const updateKomentar: UpdateKomentarDto = {
            sadrzaj: komentar.sadrzaj,
            id: props.komentar.id,
        };
        const response = await requests.putWithLoading(
            `radionice/komentari`,
            updateKomentar
        );
        if (response?.data) {
            handleClose(true);
        }
    };

    const handleClose = (reload: boolean = false) => {
        setKomentar({
            sadrzaj: "",
            radionicaId: undefined,                       //POPRAVI OVO, OVO MORA BIT OVDJEEEE!!!!!
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
