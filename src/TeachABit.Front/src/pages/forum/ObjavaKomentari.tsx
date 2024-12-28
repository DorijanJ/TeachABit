import { useEffect, useState } from "react";
import { KomentarDto } from "../../models/KomentarDto";
import requests from "../../api/agent";
import UserLink from "../profil/UserLink";
import { formatDistanceToNow } from "date-fns";
import { Button, Typography } from "@mui/material";
import TeachABitRenderer from "../../components/editor/TeachaBitRenderer";
import CreateKomentarDialog from "./CreateKomentar";
import { useGlobalContext } from "../../context/Global.context";

interface Props {
    objavaId: number;
}

export default function ObjavaKomentari(props: Props) {
    const [komentari, setKomentari] = useState<KomentarDto[]>([]);
    const [isOpenKomentarDialog, setIsOpenKomentarDialog] = useState(false);

    const getKomentarListByObjavaId = async (objavaId: number) => {
        const response = await requests.getWithLoading(
            `objave/${objavaId}/komentari`
        );
        if (response.data) {
            setKomentari(response.data);
        }
    };

    const globalContext = useGlobalContext();

    useEffect(() => {
        getKomentarListByObjavaId(props.objavaId);
    }, [props.objavaId]);

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
            <CreateKomentarDialog
                refreshData={() => getKomentarListByObjavaId(props.objavaId)}
                isOpen={isOpenKomentarDialog}
                onClose={() => setIsOpenKomentarDialog(false)}
            />

            <div
                style={{
                    display: "flex",
                    flexDirection: "column",
                    gap: "5px",
                    width: "100%",
                    alignItems: "flex-start",
                    paddingRight: "10px",
                }}
            >
                {komentari.map((komentar: KomentarDto) => (
                    <div
                        style={{
                            display: "flex",
                            flexDirection: "row",
                            gap: "20px",
                            padding: "10px",
                            alignItems: "flex-start",
                            borderTop: "1px solid gray",
                            borderColor: "lightgray",
                            width: "100%",
                        }}
                    >
                        <UserLink
                            user={{
                                id: komentar.vlasnikId,
                                username: komentar.vlasnikUsername,
                                profilnaSlikaVersion:
                                    komentar.vlasnikProfilnaSlikaVersion,
                            }}
                        />
                        <div
                            style={{
                                display: "flex",
                                flexDirection: "row",
                                justifyContent: "space-between",
                                width: "100%",
                            }}
                        >
                            <TeachABitRenderer content={komentar.sadrzaj} />

                            {komentar.createdDateTime && (
                                <p
                                    style={{
                                        margin: 0,
                                        color: "gray",
                                        fontSize: 14,
                                    }}
                                >
                                    {`${formatDistanceToNow(
                                        new Date(komentar.createdDateTime),
                                        { addSuffix: true }
                                    )} by ${komentar.vlasnikUsername}`}
                                </p>
                            )}
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
}
