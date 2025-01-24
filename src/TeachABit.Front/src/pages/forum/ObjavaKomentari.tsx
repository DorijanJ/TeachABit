import { useCallback, useEffect, useState } from "react";
import { KomentarDto } from "../../models/KomentarDto";
import requests from "../../api/agent";
import { Button, Typography } from "@mui/material";
import Komentar from "./Komentar";
import CreateKomentar from "./KomentarEditor";
import { observer } from "mobx-react";
import globalStore from "../../stores/GlobalStore";

interface Props {
    objavaId: number;
    vlasnikId: string;
}

export const ObjavaKomentari = (props: Props) => {
    const [komentari, setKomentari] = useState<KomentarDto[]>([]);
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

    const getKomentarListByObjavaId = async (objavaId: number) => {
        const response = await requests.getWithLoading(
            `objave/${objavaId}/komentari`
        );
        if (response && response.data) {
            setKomentari(response.data);
        }
    };

    const [selectedNadKomentarId, setSelectedNadKomentarId] =
        useState<number>();

    useEffect(() => {
        getKomentarListByObjavaId(props.objavaId);
    }, [props.objavaId]);

    // Recursive rendering function
    const renderKomentari = useCallback(
        (komentari: KomentarDto[], level: number) => {
            return komentari.map((komentar: KomentarDto) => (
                <div
                    style={{
                        display: "flex",
                        flexDirection: "column",
                    }}
                >
                    <Komentar
                        key={komentar.id}
                        setSelectedNadKomentarId={setSelectedNadKomentarId}
                        selectedNadKomentarId={selectedNadKomentarId}
                        komentar={komentar}
                        refreshData={() =>
                            getKomentarListByObjavaId(props.objavaId)
                        }
                        level={level}
                        collapsedComments={collapsedComments}
                        toggleCollapse={toggleCollapse}
                        objavaVlasnikId={props.vlasnikId}
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
        },
        [komentari, collapsedComments]
    );

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
                refreshData={() => getKomentarListByObjavaId(props.objavaId)}
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

export default observer(ObjavaKomentari);
