import {
    Dialog,
    DialogTitle,
    DialogContent,
    TextField,
    DialogActions,
    Button,
} from "@mui/material";
import { useState, ChangeEvent } from "react";
import requests from "../../api/agent";
import { RadionicaDto } from "../../models/RadionicaDto";
import TeachABitEditor from "../../components/editor/TeachABitTextEditor";
import { UpdateRadionicaDto } from "./UpdateRadionicaDto";

interface Props {
    refreshData: () => Promise<any>;
    onClose: () => void;
    isOpen: boolean;
    radionica?: RadionicaDto;
}

export default function RadionicaEditor(props: Props) {
    const [radionica, setRadionica] = useState<RadionicaDto>({
        naziv: props.radionica?.naziv ?? "",
        /*{sadrzaj: props.radionica?.sadrzaj ?? "",}*/
        id: props.radionica?.id,
        tema: props.radionica?.tema ?? "",
        predavacId: props.radionica?.predavacId,
        predavac: props.radionica?.predavac,
        predavacProfilnaSlika: props.radionica?.predavacProfilnaSlika,
        brojprijavljenih: props.radionica?.brojprijavljenih ?? 0,
        kapacitet: props.radionica?.kapacitet,
        datumvrijeme: props.radionica?.datumvrijeme
        
    });

    const handleClose = (reload: boolean = false) => {
        setRadionica({
            naziv: "",
            /*sadrzaj: "",*/
            tema: "",
        });
        props.onClose();
        if (reload) props.refreshData();
    };

    const handleUpdateRadionicu = async (radionica: RadionicaDto) => {
        const updateRadionicaDto: UpdateRadionicaDto = {
            id: radionica.id,
            naziv: radionica.naziv,
            /*sadrzaj: radionica.sadrzaj,*/
            tema: props.radionica?.tema ?? "",
            /*predavacProfilnaSlika: props.radionica?.predavacProfilnaSlika,*/
            brojprijavljenih: props.radionica?.brojprijavljenih ?? 0,
            kapacitet: props.radionica?.kapacitet,
            datumvrijeme: props.radionica?.datumvrijeme ?? new Date()
        };
        const response = await requests.putWithLoading(
            "radionice",
            updateRadionicaDto
        );
        if (response && response.data) {
            handleClose(true);
        }
    };

    const handleStvoriRadionicu = async (radionica: RadionicaDto) => {
        const response = await requests.postWithLoading("radionice", radionica);
        if (response && response.data) {
            handleClose(true);
        }
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
                            {`Nova radionica`}
                        </div>
                    </div>
                </DialogTitle>

                <DialogContent
                    sx={{
                        height: 600,
                        display: "flex",
                        flexDirection: "column",
                        gap: "20px",
                        paddingTop: "10px !important",
                        maxHeight: "70vh",
                        minWidth: "40vw",
                    }}
                >
                    <TextField
                        fullWidth
                        autoFocus
                        label="Naziv"
                        variant="outlined"
                        value={radionica.naziv || ""}
                        onChange={(e: ChangeEvent<HTMLInputElement>) =>
                            setRadionica((prev: any) => ({
                                ...prev,
                                naziv: e.target.value,
                            }))
                        }
                    />
                    <TeachABitEditor
                        /*content={radionica.sadrzaj}*/
                        onUpdate={(value: string) =>
                            setRadionica((prev: any) => ({
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
                            if (!radionica.id) {
                                handleStvoriRadionicu(radionica);
                            } else {
                                handleUpdateRadionicu(radionica);
                            }
                        }}
                    >
                        {radionica.id ? "Ažuriraj podatke o radionici" : "Stvori novu radionicu"}
                    </Button>
                </DialogActions>
            </Dialog>
        </>
    );
}
