import { useEffect, useState } from "react";
import requests from "../../api/agent";
import { Button, Typography } from "@mui/material";
import Komentar from "./Komentar";
import CreateKomentar from "./KomentarEditor";
import { TecajKomentarDto } from "../../models/TecajKomentarDto.ts";
import globalStore from "../../stores/GlobalStore.ts";
import AddIcon from "@mui/icons-material/Add";

interface Props {
    tecajId: number;
}

export default function TecajKomentari(props: Props) {
    const [komentari, setKomentari] = useState<TecajKomentarDto[]>([]);
    const [isOpenKomentarDialog, setIsOpenKomentarDialog] = useState(false);
    const [collapsedComments, setCollapsedComments] = useState<
        Record<number, boolean>
    >({});

    const toggleCollapse = (komentarId: number | undefined) => {
        if (komentarId === undefined) return;
        setCollapsedComments((prevState) => ({
            ...prevState,
            [komentarId]: !prevState[komentarId],
        }));
    };

    const getKomentarListByTecajId = async (tecajId: number) => {
        const response = await requests.getWithLoading(
            `tecajevi/${tecajId}/komentari`
        );
        if (response && response.data) {
            setKomentari(response.data);
        }
    };

    const [selectedNadKomentarId, setSelectedNadKomentarId] =
        useState<number>();

    useEffect(() => {
        getKomentarListByTecajId(props.tecajId);
    }, [props.tecajId]);

    // Recursive rendering function
    const renderKomentari = (komentari: TecajKomentarDto[], level: number) => {
        return komentari.map((komentar: TecajKomentarDto) => (
            <div
                key={komentar.id}
                style={{
                    display: "flex",
                    flexDirection: "column",
                }}
            >
                <Komentar
                    setSelectedNadKomentarId={setSelectedNadKomentarId}
                    selectedNadKomentarId={selectedNadKomentarId}
                    komentar={komentar}
                    refreshData={() => getKomentarListByTecajId(props.tecajId)}
                    level={level}
                    collapsedComments={collapsedComments}
                    toggleCollapse={toggleCollapse}
                />
                {/* Recursive call for nested comments */}
                {komentar.podKomentarList &&
                    komentar.id !== undefined &&
                    komentar.podKomentarList.length > 0 && (
                        <div>
                            {!collapsedComments[komentar.id] && (
                                <div>
                                    {renderKomentari(
                                        komentar.podKomentarList,
                                        level + 1
                                    )}
                                </div>
                            )}
                        </div>
                    )}
            </div>
        ));
    };

    return (
        <div
            style={{
                display: "flex",
                flexDirection: "column",
                gap: "10px",
                alignItems: "flex-start",
                width: "100%",
            }}
        >
            <hr style={{ width: "100%", margin: "0 auto" }} />
            <div
                style={{
                    display: "flex",
                    flexDirection: "row",
                    justifyContent: "space-between",
                    width: "100%",
                }}
            >
                <Typography color="textDisabled" variant="h6" component="div">
                    Komentari:
                </Typography>
                {!isOpenKomentarDialog &&
                    globalStore.currentUser !== undefined && (
                        <Button
                            onClick={() =>
                                setIsOpenKomentarDialog((prev) => !prev)
                            }
                            variant="contained"
                            sx={{
                                display: "flex",
                                alignItems: "center",
                                justifyContent: "space-between",
                                gap: "5px",
                                paddingLeft: 1,
                            }}
                        >
                            <AddIcon />
                            Dodaj Komentar
                        </Button>
                    )}
            </div>
            <CreateKomentar
                refreshData={() => getKomentarListByTecajId(props.tecajId)}
                isOpen={isOpenKomentarDialog}
                onClose={() => setIsOpenKomentarDialog(false)}
            />
            <div
                style={{
                    display: "flex",
                    flexDirection: "column",
                    width: "100%",
                }}
            >
                {renderKomentari(komentari, 0)}
            </div>
        </div>
    );
}
