import {
    Dialog,
    DialogTitle,
    DialogContent,
    TextField,
    DialogActions,
    Button,
    InputAdornment,
} from "@mui/material";
import { DemoContainer, DemoItem } from "@mui/x-date-pickers/internals/demo";
import { useState, ChangeEvent } from "react";
import requests from "../../api/agent";
import { RadionicaDto } from "../../models/RadionicaDto";
import { UpdateRadionicaDto } from "../../models/UpdateRadionicaDto";
import { DateTimePicker } from "@mui/x-date-pickers/DateTimePicker";
import { Today } from "@mui/icons-material";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider/LocalizationProvider";

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
        datumvrijeme: props.radionica?.datumvrijeme,
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
            datumvrijeme: props.radionica?.datumvrijeme ?? new Date(),
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
                id="radionicaEditor"
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
                        minWidth: "50vw",
                    }}

                /*
                  bilo bi dobro tu imat nekakvu sliku al je nema trenutno
                */
                >
                    <TextField
                        fullWidth
                        autoFocus
                        label="Naziv radionice"
                        name="naziv"
                        variant="outlined"
                        //required = {true}
                        value={radionica.naziv || ""}
                        onChange={(e: ChangeEvent<HTMLInputElement>) =>
                            setRadionica((prev: any) => ({
                                ...prev,
                                naziv: e.target.value,
                            }))
                        }
                    />

                    <TextField
                        fullWidth
                        autoFocus
                        label="Tema radionice"
                        name="tema"
                        variant="outlined"
                        multiline
                        rows={2}
                        //required = {true}
                        value={radionica.tema || ""}
                        onChange={(e: ChangeEvent<HTMLInputElement>) =>
                            setRadionica((prev: any) => ({
                                ...prev,
                                tema: e.target.value,
                            }))
                        }
                    />

                    <TextField
                        fullWidth
                        autoFocus
                        label="Kapacitet radionice"
                        variant="outlined"
                        //required = {true}
                        value={radionica.kapacitet || ""}
                        onChange={(e: ChangeEvent<HTMLInputElement>) => {
                            const numericValue = e.target.value.replace(/[^0-9]/g, "");
                            setRadionica((prev: any) => ({
                                ...prev,
                                kapacitet: numericValue,
                            }));
                        }}
                    />

                    <TextField
                        label="Cijena"
                        name="cijena"
                        sx={{
                            width: "90%",
                        }}
                        variant="outlined"
                        value={radionica.cijena?.toString()}
                        type="number"
                        slotProps={{
                            input: {
                                startAdornment: (
                                    <InputAdornment position="start">€</InputAdornment>
                                ),
                            },
                        }}
                        onChange={(e: ChangeEvent<HTMLInputElement>) => {
                            const value = parseFloat(e.target.value);
                            const decimalPlaces = value.toString().split(".")[1]?.length;
                            if (!(Math.floor(value) === value) && decimalPlaces > 2) {
                                return;
                            }
                            setRadionica((prev: any) => ({
                                ...prev,
                                cijena: e.target.value,
                            }));
                        }}
                    />
                </DialogContent>

                <DialogActions>
                    <Button variant="outlined" onClick={() => handleClose()}>
                        Odustani
                    </Button>
                    <Button
                        id="stvoriRadionicuButton"
                        variant="contained"
                        onClick={() => {
                            if (!radionica.id) {
                                handleStvoriRadionicu(radionica);
                            } else {
                                handleUpdateRadionicu(radionica);
                            }
                        }}
                    >
                        {radionica.id
                            ? "Ažuriraj podatke o radionici"
                            : "Stvori novu radionicu"}
                    </Button>
                </DialogActions>
            </Dialog>
        </>
    );
}
