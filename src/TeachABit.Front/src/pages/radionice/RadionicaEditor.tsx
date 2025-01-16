import {
    Dialog,
    DialogTitle,
    DialogContent,
    TextField,
    DialogActions,
    Button,
    InputAdornment,
} from "@mui/material";
import { useState, ChangeEvent } from "react";
import requests from "../../api/agent";
import { RadionicaDto } from "../../models/RadionicaDto";
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
    });

    const handleClose = (reload: boolean = false) => {
        setRadionica({
            naziv: "",
            opis: props.radionica?.opis ?? "",
        });
        props.onClose();
        if (reload) props.refreshData();
    };

    const handleUpdateRadionicu = async (radionica: RadionicaDto) => {
        const updateRadionicaDto: UpdateRadionicaDto = {
            id: radionica.id,
            opis: radionica.opis,
            naziv: radionica.naziv,
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
                    <TextField
                        label="Opis"
                        name="opis"
                        required={true}
                        fullWidth
                        multiline
                        rows={4}
                        variant="outlined"
                        onChange={(e: ChangeEvent<HTMLInputElement>) =>
                            setRadionica((prev: any) => ({
                                ...prev,
                                opis: e.target.value,
                            }))
                        }
                    />
                    <TextField
                        label="Cijena"
                        name="cijena"
                        sx={{
                            width: "200px",
                        }}
                        variant="outlined"
                        value={radionica.cijena?.toString()}
                        type="number"
                        slotProps={{
                            input: {
                                startAdornment: (
                                    <InputAdornment position="start">
                                        €
                                    </InputAdornment>
                                ),
                            },
                        }}
                        onChange={(e: ChangeEvent<HTMLInputElement>) => {
                            const value = parseFloat(e.target.value);
                            const decimalPlaces = value
                                .toString()
                                .split(".")[1]?.length;
                            if (
                                !(Math.floor(value) === value) &&
                                decimalPlaces > 2
                            ) {
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
