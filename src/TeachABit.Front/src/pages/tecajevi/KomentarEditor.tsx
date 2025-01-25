import {
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
} from "@mui/material";
import { useMemo, useState } from "react";
import { TecajKomentarDto } from "../../models/TecajKomentarDto.ts";
import TeachABitEditor from "../../components/editor/TeachABitTextEditor";
import { useParams } from "react-router-dom";
import requests from "../../api/agent";
import { UpdateKomentarDto } from "../../models/UpdateKomentarDto";

const maxSadrzajLength = 1000;

interface Props {
    refreshData: () => Promise<any>;
    onClose: () => void;
    isOpen: boolean;
    nadKomentarId?: number | undefined;
    komentar?: TecajKomentarDto;
}

export default function KomentarEditor(props: Props) {
    const [komentar, setKomentar] = useState<TecajKomentarDto>({
        sadrzaj: props.komentar?.sadrzaj ?? "",
    });

    const { tecajId } = useParams();

    const parsedTecajId = useMemo(() => {
        if (!tecajId) return undefined;
        return parseInt(tecajId);
    }, [tecajId]);

    const isValidSadrzaj = useMemo(() => {
        const sadrzaj = komentar.sadrzaj;
        if (sadrzaj.length == 0 || sadrzaj.length > maxSadrzajLength) return false;
        return true;
    }, [komentar.sadrzaj]);

    const handleStvoriKomentar = async () => {

        if (!parsedTecajId) {
            console.log(parsedTecajId);
            return;}
        if (props.nadKomentarId) komentar.nadKomentarId = props.nadKomentarId;
        const response = await requests.postWithLoading(
            `tecajevi/${parsedTecajId}/komentari`,
            komentar
        );
        if (response && response.data) {
            handleClose(true);
        }
    };

    const handleUpdateKomentar = async () => {
        if (!parsedTecajId || !props.komentar) return;
        const updateKomentar: UpdateKomentarDto = {
            sadrzaj: komentar.sadrzaj,
            id: props.komentar.id,
        };
        const response = await requests.putWithLoading(
            `tecajevi/komentari`,
            updateKomentar
        );
        if (response?.data) {
            handleClose(true);
        }
    };

    const handleClose = (reload: boolean = false) => {
        setKomentar({
            sadrzaj: "",
            //POPRAVI OVO, OVO MORA BIT OVDJEEEE!!!!!
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
                        disabled={!isValidSadrzaj}
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
