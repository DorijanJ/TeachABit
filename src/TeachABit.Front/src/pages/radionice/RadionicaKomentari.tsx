import { useEffect, useState } from "react";
import { RadionicaKomentarDto } from "../../models/RadionicaKomentarDto";
import requests from "../../api/agent";
import { Button, Typography } from "@mui/material";
import Komentar from "./Komentar";
import CreateKomentar from "./KomentarEditor";
import globalStore from "../../stores/GlobalStore";
import { observer } from "mobx-react";

interface Props {
    radionicaId: number;
}

export const RadionicaKomentari = (props: Props) => {
    const [komentari, setKomentari] = useState<RadionicaKomentarDto[]>([]);
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

    const getKomentarListByRadionicaId = async (radionicaId: number) => {
        const response = await requests.getWithLoading(
            `radionice/${radionicaId}/komentari`
        );
        if (response && response.data) {
            setKomentari(response.data);
        }
    };

    const [selectedNadKomentarId, setSelectedNadKomentarId] =
        useState<number>();

    useEffect(() => {
        getKomentarListByRadionicaId(props.radionicaId);
    }, [props.radionicaId]);

    const renderKomentari = (
        komentari: RadionicaKomentarDto[],
        level: number
    ) => {
        return komentari.map((komentar: RadionicaKomentarDto) => (
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
                    refreshData={() =>
                        getKomentarListByRadionicaId(props.radionicaId)
                    }
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
                        >
                            Dodaj Komentar
                        </Button>
                    )}
            </div>
            <CreateKomentar
                refreshData={() =>
                    getKomentarListByRadionicaId(props.radionicaId)
                }
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
};

export default observer(RadionicaKomentari);
