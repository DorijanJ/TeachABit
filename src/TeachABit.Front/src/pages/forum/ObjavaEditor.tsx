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
import { ObjavaDto } from "../../models/ObjavaDto";
import TeachABitEditor from "../../components/editor/TeachABitTextEditor";
import { UpdateObjavaDto } from "../../models/UpdateObjavaDto";

interface Props {
    refreshData: () => Promise<any>;
    onClose: () => void;
    isOpen: boolean;
    objava?: ObjavaDto;
}

export default function ObjavaEditor(props: Props) {
    const [objava, setObjava] = useState<ObjavaDto>({
        naziv: props.objava?.naziv ?? "",
        sadrzaj: props.objava?.sadrzaj ?? "",
        id: props.objava?.id,
    });

    const handleClose = (reload: boolean = false) => {
        setObjava({
            naziv: "",
            sadrzaj: "",
        });
        props.onClose();
        if (reload) props.refreshData();
    };

    const handleUpdateObjava = async (objava: ObjavaDto) => {
        const updateObjavaDto: UpdateObjavaDto = {
            id: objava.id,
            naziv: objava.naziv,
            sadrzaj: objava.sadrzaj,
        };
        const response = await requests.putWithLoading(
            "objave",
            updateObjavaDto
        );
        if (response && response.data) {
            handleClose(true);
        }
    };

    const handleStvoriObjavu = async (objava: ObjavaDto) => {
        const response = await requests.postWithLoading("objave", objava);
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
                id="objavaEditor"
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
                            {`Nova objava`}
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
                        name="naziv"
                        value={objava.naziv || ""}
                        onChange={(e: ChangeEvent<HTMLInputElement>) =>
                            setObjava((prev: any) => ({
                                ...prev,
                                naziv: e.target.value,
                            }))
                        }
                    />
                    <TeachABitEditor
                        content={objava.sadrzaj}
                        onUpdate={(value: string) =>
                            setObjava((prev: any) => ({
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
                        id="objavaEditorStvoriObjavu"
                        variant="contained"
                        onClick={() => {
                            if (!objava.id) {
                                handleStvoriObjavu(objava);
                            } else {
                                handleUpdateObjava(objava);
                            }
                        }}
                    >
                        {objava.id ? "Ažuriraj objavu" : "Stvori objavu"}
                    </Button>
                </DialogActions>
            </Dialog>
        </>
    );
}
