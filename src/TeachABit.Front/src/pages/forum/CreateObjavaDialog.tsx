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

interface Props {
    refreshData: () => Promise<any>;
    onClose: () => void;
    isOpen: boolean;
}

export default function CreateObjavaDialog(props: Props) {
    const [objava, setObjava] = useState<ObjavaDto>({
        sadrzaj: "",
        naziv: "",
    });

    const handleClose = (reload: boolean = false) => {
        setObjava({
            naziv: "",
            sadrzaj: "",
        });
        props.onClose();
        if (reload) props.refreshData();
    };

    const handleStvoriObjavu = async (objava: ObjavaDto) => {
        const response = await requests.postWithLoading("objave", objava);
        if (response.data) {
            handleClose(true);
        }
    };

    return (
        <>
            {props.isOpen && (
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
                        {!objava.id && (
                            <Button
                                variant="contained"
                                onClick={() => handleStvoriObjavu(objava)}
                            >
                                Stvori objavu
                            </Button>
                        )}
                    </DialogActions>
                </Dialog>
            )}
        </>
    );
}
