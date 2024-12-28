import { useEffect, useState } from "react";
import { KomentarDto } from "../../models/KomentarDto";
import requests from "../../api/agent";
import { useGlobalContext } from "../../context/Global.context";
import { Button, Typography } from "@mui/material";
import Komentar from "./Komentar";
import CreateKomentar from "./CreateKomentar";

interface Props {
    objavaId: number;
}

export default function ObjavaKomentari(props: Props) {
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
        if (response.data) {
            setKomentari(response.data);
        }
    };

    const globalContext = useGlobalContext();

    const [selectedNadKomentarId, setSelectedNadKomentarId] =
        useState<number>();

    useEffect(() => {
        getKomentarListByObjavaId(props.objavaId);
    }, [props.objavaId]);

    // Recursive rendering function
    const renderKomentari = (komentari: KomentarDto[], level: number) => {
        return komentari.map((komentar: KomentarDto) => (
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
                        getKomentarListByObjavaId(props.objavaId)
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
                gap: "20px",
                alignItems: "flex-start",
                width: "100%",
            }}
        >
            <div
                style={{
                    display: "flex",
                    flexDirection: "row",
                    justifyContent: "space-between",
                    width: "100%",
                }}
            >
                <Typography color="primary" variant="h5" component="div">
                    Komentari
                </Typography>
                {!isOpenKomentarDialog && globalContext.userIsLoggedIn && (
                    <Button
                        onClick={() => setIsOpenKomentarDialog((prev) => !prev)}
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
}
