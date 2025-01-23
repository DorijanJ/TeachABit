import Dialog from "@mui/material/Dialog";
import {
    Button,
    DialogActions,
    DialogContent,
    DialogTitle,
    InputAdornment,
    TextField,
} from "@mui/material";
import localStyles from "../../components/auth/form/AuthForm.module.css";
import { ChangeEvent, useEffect, useMemo, useState } from "react";
import requests from "../../api/agent.ts";
import { CreateOrUpdateTecajDto } from "../../models/CreateOrUpdateTecajDto.ts";
import ImageUploadComponent from "../../components/ImageUploadComponent.tsx";
import { TecajDto } from "../../models/TecajDto.ts";
import TeachABitEditor from "../../components/editor/TeachABitTextEditor.tsx";
import { useNavigate } from "react-router-dom";

interface Props {
    refreshData: () => Promise<any>;
    onClose: () => void;
    isOpen: boolean;
    tecaj?: TecajDto;
    editing?: boolean;
}

export default function TecajPopup(props: Props) {
    const [tecaj, setTecaj] = useState<CreateOrUpdateTecajDto>({
        naziv: props.tecaj?.naziv ?? "",
        id: props.tecaj?.id,
        opis: props.tecaj?.opis ?? "",
        cijena: props.tecaj?.cijena,
        naslovnaSlikaBase64: "",
    });

    const navigate = useNavigate();

    useEffect(() => {
        if (props.tecaj) {
            setTecaj({
                naziv: props.tecaj.naziv ?? "",
                id: props.tecaj.id,
                opis: props.tecaj.opis ?? "",
                cijena: props.tecaj?.cijena,
                naslovnaSlikaBase64: "",
            });
        }
    }, [props.tecaj]);

    const handleClose = (reload: boolean = false) => {
        setTecaj({
            naziv: "",
            opis: "",
            cijena: undefined,
            naslovnaSlikaBase64: "",
        });
        props.onClose();
        if (reload) props.refreshData();
    };

    const handleStvoriTecaj = async (tecaj: CreateOrUpdateTecajDto) => {
        const response = await requests.postWithLoading("tecajevi", tecaj);
        if (response && response.data) {
            navigate(`/tecajevi/${response.data.id}`);
            props.onClose();
        }
    };

    const handleAzurirajTecaj = async (tecaj: CreateOrUpdateTecajDto) => {
        const response = await requests.putWithLoading("tecajevi", tecaj);
        if (response && response.data) {
            handleClose(true);
        }
    };

    const isValidPrice = useMemo(() => {
        const value = tecaj.cijena;
        if (!value) return true;
        if (isNaN(value) || value <= 0) {
            return false;
        }

        return true;
    }, [tecaj.cijena]);

    const isEmptyNaziv = useMemo(() => {
        const naziv = tecaj.naziv;
        if (naziv.length == 0) return true;
        return false;
    }, [tecaj.naziv]);

    return (
        <Dialog
            open={props.isOpen}
            onClose={() => handleClose(true)}
            maxWidth={"md"}
        >
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
                        {props.editing ? `Uredi tečaj` : `Stvori tečaj`}
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
                    defaultValue={tecaj.naziv}
                    onChange={(e: ChangeEvent<HTMLInputElement>) =>
                        setTecaj((prev: any) => ({
                            ...prev,
                            naziv: e.target.value,
                        }))
                    }
                />
                <label>Opis:</label>
                <TeachABitEditor
                    content={tecaj.opis}
                    onUpdate={(v) =>
                        setTecaj((prev: any) => ({
                            ...prev,
                            opis: v,
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
                    //value={tecaj.cijena}
                    defaultValue={tecaj.cijena}
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
                            cijena:
                                e.target.value !== ""
                                    ? e.target.value ?? undefined
                                    : undefined,
                        }));
                    }}
                />

                <div
                    style={{
                        display: "flex",
                        flexDirection: "column",
                        gap: "10px",
                    }}
                >
                    <ImageUploadComponent
                        setFile={(file: string) => {
                            setTecaj((prev: CreateOrUpdateTecajDto) => ({
                                ...prev,
                                naslovnaSlikaBase64: file,
                            }));
                        }}
                        ratio="2/1"
                        width={"70%"}
                    />
                </div>

                <DialogActions>
                    <Button
                        className={localStyles.myButton}
                        variant="outlined"
                        onClick={() => handleClose(true)}
                    >
                        Odustani
                    </Button>
                    <Button
                        disabled={!isValidPrice || isEmptyNaziv}
                        className={localStyles.myButton}
                        variant="contained"
                        onClick={() => {
                            if (!tecaj.id) {
                                handleStvoriTecaj(tecaj);
                            } else if (props.editing) {
                                handleAzurirajTecaj(tecaj);
                            }
                        }}
                    >
                        {props.editing ? `Spremi promjene` : `Stvori tečaj`}
                    </Button>
                </DialogActions>
            </DialogContent>
        </Dialog>
    );
}
