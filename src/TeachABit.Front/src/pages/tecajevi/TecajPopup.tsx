
import Dialog from "@mui/material/Dialog";
import { Box, Button, DialogContent, DialogTitle, TextField } from "@mui/material";
import localStyles from "../../components/auth/form/AuthForm.module.css";

import {TecajDto} from "../../models/TecajDto.ts";
import {ChangeEvent, useState} from "react";
import requests from "../../api/agent.ts";

interface Props {
    refreshData: () =>  Promise<any>;
    onClose: () => void;
    isOpen: boolean;
    tecajObj?: TecajDto;
}

export default function TecajPopup(props: Props) {

    const [objava, setObjava] = useState<TecajDto>({
        naziv: props.tecajObj?.naziv ?? "",
        id: props.tecajObj?.id ?? 0, // Fallback to 0 if id is undefined
    });

    const handleClose = (reload: boolean = false) => {
        setObjava({
            naziv: "",
            id: 0,
        });
        props.onClose();
        if (reload) props.refreshData();
    };

    const handleStvoriObjavu = async (objava: TecajDto) => {
        const response = await requests.postWithLoading("tecajevi", objava);
        if (response && response.data) {
            handleClose(true);
        }
    };

    return (
        <Dialog
            open={props.isOpen}
            onClose={props.onClose}
            sx={{
                "& .MuiPaper-root": {
                    width: "40vw",
                    margin: "2rem",
                },
            }}
        >
            <DialogTitle
                sx={{
                    textAlign: "center",
                    margin: "1rem",
                    fontWeight: "bold",
                    fontSize: "2rem"
                }}
            >
                Stvori tečaj
            </DialogTitle>
            <DialogContent>
                <Box
                    sx={{
                        display: "flex",
                        flexDirection: "column",
                        m: "auto",
                    }}
                >
                    <TextField
                        label="Naziv"
                        name="tecaj"
                        color="secondary"
                        required={true}
                        fullWidth
                        sx={{
                            fontSize: "1.5rem",
                            marginBottom: "1rem",
                        }}
                        onChange={(e: ChangeEvent<HTMLInputElement>) =>
                            setObjava((prev: any) => ({
                                ...prev,
                                naziv: e.target.value,
                            }))
                        }
                    />
                    <TextField
                        label="Opis"
                        name="tecaj"
                        color="secondary"
                        required={true}
                        fullWidth
                        multiline
                        rows={4}
                        variant="outlined"
                        sx={{
                            fontSize: "16px",
                            minHeight: "100px",
                        }}
                    />
                    <Box sx={{ display: "flex", gap: 2, mt: 2, justifyContent: "space-evenly" }}>
                        <Button
                            className={localStyles.myButton}
                            variant="outlined"
                            onClick={props.onClose} // Close dialog on cancel
                            color="error"
                        >
                            Odustani
                        </Button>
                        <Button className={localStyles.myButton}
                                variant="contained"
                                color="secondary"
                                onClick={() => {
                                if (objava.id!=0) {
                                    setObjava((prev: any) => ({
                                        ...prev,
                                        id: 1,
                                    }))
                                    handleStvoriObjavu(objava);
                                }}}>
                            Stvori
                        </Button>
                    </Box>
                </Box>
            </DialogContent>
        </Dialog>
    );
}


