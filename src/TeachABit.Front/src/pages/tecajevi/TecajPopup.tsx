import Dialog from "@mui/material/Dialog";
import {
    Box,
    Button,
    DialogActions,
    DialogContent,
    DialogTitle,
    InputAdornment,
    TextField,
} from "@mui/material";
import localStyles from "../../components/auth/form/AuthForm.module.css";

import { TecajDto } from "../../models/TecajDto.ts";
import { ChangeEvent, useMemo, useState } from "react";
import requests from "../../api/agent.ts";

interface Props {
    refreshData: () => Promise<any>;
    onClose: () => void;
    isOpen: boolean;
    tecaj?: TecajDto;
}

export default function TecajPopup(props: Props) {
    const [tecaj, setTecaj] = useState<TecajDto>({
        naziv: props.tecaj?.naziv ?? "",
        id: props.tecaj?.id,
        opis: props.tecaj?.opis ?? "",
    });

    const handleClose = (reload: boolean = false) => {
        setTecaj({
            naziv: "",
            opis: "",
        });
        props.onClose();
        if (reload) props.refreshData();
    };

    const handleStvoriTecaj = async (tecaj: TecajDto) => {
        const response = await requests.postWithLoading("tecajevi", tecaj);
        if (response && response.data) {
            handleClose(true);
        }
    };

    const isValidPrice = useMemo(() => {
        const value = tecaj.cijena;
        if (value === undefined) return true;
        if (isNaN(value) || value <= 0) {
            return false;
        }

        return true;
    }, [tecaj.cijena]);

    return (
        <Dialog open={props.isOpen} onClose={props.onClose} maxWidth={"md"}>
            <DialogTitle
                sx={{
                    maxWidth: "100%",
                }}
            >
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
                        {`Stvori tečaj`}
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
                    label="Naziv"
                    name="naziv"
                    variant="outlined"
                    required={true}
                    fullWidth
                    onChange={(e: ChangeEvent<HTMLInputElement>) =>
                        setTecaj((prev: any) => ({
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
                        setTecaj((prev: any) => ({
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
                    value={tecaj.cijena?.toString()}
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
                        setTecaj((prev: any) => ({
                            ...prev,
                            cijena: e.target.value,
                        }));
                    }}
                />

                <DialogActions>
                    <Button
                        className={localStyles.myButton}
                        variant="outlined"
                        onClick={props.onClose}
                    >
                        Odustani
                    </Button>
                    <Button
                        disabled={!isValidPrice}
                        className={localStyles.myButton}
                        variant="contained"
                        onClick={() => {
                            if (!tecaj.id) {
                                handleStvoriTecaj(tecaj);
                            }
                        }}
                    >
                        Stvori
                    </Button>
                </DialogActions>
            </DialogContent>
        </Dialog>
    );
}
