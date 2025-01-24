import {
    Dialog,
    DialogTitle,
    DialogContent,
    DialogActions,
    Button,
    TextField,
} from "@mui/material";
import { RadionicaDto } from "../../models/RadionicaDto";

import { ObavijestDto } from "../../models/ObavijestDto";
import { useState } from "react";
import requests from "../../api/agent";

interface Props {
    onConfirm: (obavijest: ObavijestDto) => Promise<any>;
    onClose: () => void;
    radionica: RadionicaDto;
    obavijest: ObavijestDto;
}

export default function RadionicaPopup(props: Props) {
    const [obavijest, setObavijest] = useState<ObavijestDto>(
        props.obavijest || {
            naslov: "",
            poruka: "",
            radionicaId: props.radionica.id,
        }
    );

    const handleConfirm = async () => {
        if (!props.radionica.id) return;
        const o = obavijest;
        o.radionicaId = props.radionica.id;
        await requests.postWithLoading(
            `${props.radionica.id}/obavijest`,
            obavijest
        );
        props.onClose();
    };
    return (
        <>
            <Dialog open onClose={props.onClose} maxWidth="md" fullWidth>
                <DialogTitle>
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
                </DialogTitle>

                <DialogContent>
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
                </DialogContent>

                <DialogActions>
                    <Button variant="outlined" onClick={props.onClose}>
                        Odustani
                    </Button>
                    <Button variant="contained" onClick={handleConfirm}>
                        Pošalji
                    </Button>
                </DialogActions>
            </Dialog>
        </>
    );
}
