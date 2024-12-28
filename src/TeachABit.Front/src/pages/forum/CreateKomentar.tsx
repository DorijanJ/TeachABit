import { Button, DialogContent } from "@mui/material";
import { useState } from "react";
import { KomentarDto } from "../../models/KomentarDto";
import TeachABitEditor from "../../components/editor/TeachABitTextEditor";
import { useParams } from "react-router-dom";
import requests from "../../api/agent";

interface Props {
    refreshData: () => Promise<any>;
    onClose: () => void;
    isOpen: boolean;
    nadKomentarId?: number | undefined;
}

export default function CreateKomentar(props: Props) {
    const [komentar, setKomentar] = useState<KomentarDto>({
        sadrzaj: "",
    });

    const { objavaId } = useParams();

    const handleStvoriKomentar = async () => {
        if (!objavaId) return;
        if (props.nadKomentarId) komentar.nadKomentarId = props.nadKomentarId;
        komentar.objavaId = parseInt(objavaId);
        const response = await requests.postWithLoading(
            `objave/${objavaId}/komentari`,
            komentar
        );
        if (response.data) {
            handleClose(true);
        }
    };

    const handleClose = (reload: boolean = false) => {
        setKomentar({
            sadrzaj: "",
            objavaId: undefined,
        });
        props.onClose();
        if (reload) props.refreshData();
    };

    return (
        <>
            {props.isOpen && (
                <div style={{ width: "100%" }}>
                    <DialogContent
                        sx={{
                            width: "100%",
                            display: "flex",
                            flexDirection: "column",
                            gap: "20px",
                            paddingTop: "10px !important",
                            maxHeight: 300,
                            scrollbarGutter: "stable",
                        }}
                    >
                        <TeachABitEditor
                            content={komentar.sadrzaj}
                            onUpdate={(value: string) =>
                                setKomentar((prev: any) => ({
                                    ...prev,
                                    sadrzaj: value,
                                }))
                            }
                        />
                    </DialogContent>
                    <div
                        style={{
                            display: "flex",
                            flexDirection: "row",
                            justifyContent: "flex-end",
                            gap: 20,
                            padding: "20px 0",
                        }}
                    >
                        <Button
                            variant="outlined"
                            onClick={() => handleClose()}
                        >
                            Odustani
                        </Button>
                        <Button
                            variant="contained"
                            onClick={handleStvoriKomentar}
                        >
                            Stvori komentar
                        </Button>
                    </div>
                </div>
            )}
        </>
    );
}
