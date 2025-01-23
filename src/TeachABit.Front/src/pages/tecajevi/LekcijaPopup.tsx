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
import { LekcijaDto } from "../../models/LekcijaDto.ts";
import TeachABitEditor from "../../components/editor/TeachABitTextEditor.tsx";

const maxNazivLength = 500;
const maxSadrzajLength = 20000;

interface Props {
    refreshData: () => Promise<any>;
    onClose: () => void;
    isOpen: boolean;
    lekcija?: LekcijaDto;
    tecajId: number;
    editing?: boolean;
}

export default function LekcijaPopup(props: Props) {
    const [lekcija, setLekcija] = useState<LekcijaDto>({
        id: props.lekcija?.id,
        naziv: props.lekcija?.naziv ?? "",
        sadrzaj: props.lekcija?.sadrzaj ?? "",
        tecajId: props.lekcija?.tecajId ?? props.tecajId,
    });

    useEffect(() => {
        if (props.lekcija) {
            setLekcija({
                id: props.lekcija?.id,
                naziv: props.lekcija?.naziv ?? "",
                sadrzaj: props.lekcija?.sadrzaj ?? "",
                tecajId: props.lekcija.tecajId ?? props.tecajId,
            });
        }
    }, [props.lekcija]);

    const handleClose = (reload: boolean = false) => {
        setLekcija({
            id: undefined,
            naziv: "",
            sadrzaj: "",
            tecajId: props.tecajId,
        });
        props.onClose();
        if (reload) props.refreshData();
    };

    const handleStvoriLekcija = async (lekcija: LekcijaDto) => {
        if (lekcija.tecajId) {
            const response = await requests.postWithLoading(
                `tecajevi/${lekcija.tecajId}/lekcije`,
                lekcija
            );
            if (response && response.data) {
                handleClose(true);
            }
        }
    };

    const handleAzurirajLekcija = async (lekcija: LekcijaDto) => {
        if (lekcija.tecajId) {
            const response = await requests.putWithLoading(
                "tecajevi/lekcije",
                lekcija
            );
            if (response && response.data) {
                handleClose(true);
            }
        }
    };

    const isValidNaziv = useMemo(() => {
        const naziv = lekcija.naziv;
        if (naziv.length == 0 || naziv.length > maxNazivLength) return false;
        return true;
    }, [lekcija.naziv]);

    const isValidSadrzaj = useMemo(() => {
        const sadrzaj = lekcija.sadrzaj;
        if (sadrzaj.length == 0 || sadrzaj.length > maxSadrzajLength) return false;
        return true;
    }, [lekcija.sadrzaj]);

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
                        {props.editing ? `Uredi lekciju` : `Stvori lekciju`}
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
                {/* Polja za naziv i sadrzaj */}
                <TextField
                    label="Naziv"
                    name="naziv"
                    variant="outlined"
                    required={true}
                    fullWidth
                    value={lekcija.naziv}
                    onChange={(e: ChangeEvent<HTMLInputElement>) =>
                        setLekcija((prev: any) => ({
                            ...prev,
                            naziv: e.target.value,
                        }))
                    }
                />
                <label>Sadržaj:</label>
                <TeachABitEditor
                    content={props.lekcija?.sadrzaj}
                    onUpdate={(v) =>
                        setLekcija((prev: any) => ({
                            ...prev,
                            sadrzaj: v,
                        }))
                    }
                />

                {/* Save i cancel gumbi */}
                <DialogActions>
                    <Button
                        className={localStyles.myButton}
                        variant="outlined"
                        onClick={() => handleClose(true)}
                    >
                        Odustani
                    </Button>
                    <Button
                        disabled={!isValidNaziv || !isValidSadrzaj}
                        className={localStyles.myButton}
                        variant="contained"
                        onClick={() => {
                            if (!lekcija.id) {
                                handleStvoriLekcija(lekcija);
                            } else if (props.editing) {
                                handleAzurirajLekcija(lekcija);
                            }
                        }}
                    >
                        {props.editing ? `Spremi promjene` : `Stvori lekciju`}
                    </Button>
                </DialogActions>
            </DialogContent>
        </Dialog>
    );
}
