import {
    Dialog,
    DialogContent,
    DialogActions,
    Button,
    TextField,
} from "@mui/material";
import { RadionicaDto } from "../../models/RadionicaDto";

import { ObavijestDto } from "../../models/ObavijestDto";
import { useState } from "react";
import { Info } from "@mui/icons-material";

interface Props {
    onConfirm: (obavijest: ObavijestDto) => Promise<any>;
    onClose: () => void;
    radionica: RadionicaDto;
}

export default function RadionicaUputePopup(props: Props) {
    const [obavijest, setObavijest] = useState<ObavijestDto>({
        naslov: "",
        poruka: "",
        radionicaId: props.radionica.id ?? 0,
    });

    return (
        <>
            <Dialog open onClose={props.onClose} maxWidth="md" fullWidth>
                <DialogContent
                    sx={{
                        display: "flex",
                        flexDirection: "column",
                        gap: "20px",
                    }}
                >
                    <TextField
                        fullWidth
                        autoFocus
                        label="Naslov obavijesti"
                        variant="outlined"
                        value={obavijest.naslov}
                        onChange={(e) =>
                            setObavijest((prev: any) => ({
                                ...prev,
                                naslov: e.target.value,
                            }))
                        }
                    />
                    <TextField
                        fullWidth
                        label="Opis obavijesti"
                        variant="outlined"
                        multiline
                        rows={4}
                        value={obavijest.poruka}
                        onChange={(e) =>
                            setObavijest((prev: any) => ({
                                ...prev,
                                poruka: e.target.value,
                            }))
                        }
                    />
                    <div
                        style={{
                            display: "flex",
                            flexDirection: "row",
                            alignItems: "center",
                            gap: "10px",
                        }}
                    >
                        <Info />
                        {
                            "Ovu obavijest možete poslati samo jednom i primiti će ju svi korisnici koji su kupili radionicu."
                        }
                    </div>
                </DialogContent>

                <DialogActions>
                    <Button variant="outlined" onClick={props.onClose}>
                        Odustani
                    </Button>
                    <Button
                        disabled={
                            obavijest.naslov.length === 0 ||
                            obavijest.poruka.length === 0
                        }
                        variant="contained"
                        onClick={() => props.onConfirm(obavijest)}
                    >
                        Pošalji
                    </Button>
                </DialogActions>
            </Dialog>
        </>
    );
}
