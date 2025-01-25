import Dialog from "@mui/material/Dialog";
import {
    Button,
    DialogActions,
    DialogContent,
    DialogTitle,
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
import CijenaFancyInput from "./CijenaFancyInput.tsx";

const maxNazivLength = 500;
const maxOpisLength = 10000;
const minCijena = 0;
const maxCijena = 2000;

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
        cijena: props.tecaj?.cijena ?? 0,
        naslovnaSlikaBase64: "",
    });

    const navigate = useNavigate();

    useEffect(() => {
        if (props.tecaj) {
            setTecaj({
                naziv: props.tecaj.naziv ?? "",
                id: props.tecaj.id,
                opis: props.tecaj.opis ?? "",
                cijena: props.tecaj?.cijena ?? 0,
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
        if (
            value == undefined ||
            isNaN(value) ||
            value < minCijena ||
            value > maxCijena
        ) {
            return false;
        }

        return true;
    }, [tecaj.cijena]);

    const isValidNaziv = useMemo(() => {
        const naziv = tecaj.naziv;
        if (naziv.length == 0 || naziv.length > maxNazivLength) return false;
        return true;
    }, [tecaj.naziv]);

    const isValidOpis = useMemo(() => {
        const opis = tecaj.opis;
        if (opis.length == 0 || opis.length > maxOpisLength) return false;
        return true;
    }, [tecaj.opis]);

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
                    value={tecaj.naziv}
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

                <CijenaFancyInput tecaj={tecaj} setTecaj={setTecaj} />

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
                        disabled={
                            !isValidPrice || !isValidNaziv || !isValidOpis
                        }
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
