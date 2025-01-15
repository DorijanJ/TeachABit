import { useEffect, useState } from "react";
import { TecajDto } from "../../models/TecajDto";
import requests from "../../api/agent";
import { Box, Button } from "@mui/material";
import { useGlobalContext } from "../../context/Global.context";
import Tecaj from "./Tecaj";
import SearchBox from "../../components/searchbox/SearchBox";
import useRequestBuilder from "../../hooks/useRequestBuilder";
import TecajPopup from "./TecajPopup";

export default function Tecajevi() {
    const [tecajList, setTecajList] = useState<TecajDto[]>([]);
    const globalContext = useGlobalContext();

    const { buildRequest } = useRequestBuilder();

    const [dialogOpen, setDialogOpen] = useState(false);

    const handleOpen = () => setDialogOpen(true);
    const handleClose = () => setDialogOpen(false);

    const GetTecajList = async (search: string | undefined = undefined) => {
        const response = await requests.getWithLoading(
            buildRequest("tecajevi", { search })
        );
        if (response && response.data) setTecajList(response.data);
    };

    useEffect(() => {
        GetTecajList();
    }, []);

    return (
        <div
            style={{
                display: "flex",
                flexDirection: "column",
                gap: "20px",
                alignItems: "center",
                height: "100%",
                width: "100%",
            }}
        >
            <div
                style={{
                    display: "flex",
                    flexDirection: "row",
                    gap: "20px",
                    alignItems: "center",
                    width: "50%",
                }}
            >
                <SearchBox onSearch={GetTecajList} />

                {globalContext.userIsLoggedIn && (
                    <Button
                        variant="contained"
                        onClick={() => {
                            handleOpen();
                        }}
                    >
                        Stvori tecaj
                    </Button>
                )}
                <TecajPopup
                    isOpen={dialogOpen}
                    onClose={handleClose}
                    refreshData={() => GetTecajList()}
                />
            </div>
            <div
                style={{
                    display: "flex",
                    flexDirection: "row",
                    flexWrap: "wrap",
                    gap: "20px",
                    maxWidth: "100%",
                    width: "100%",
                }}
            >
                {tecajList.map((tecaj) => (
                    <Tecaj
                        key={"tecaj" + tecaj.id}
                        tecaj={tecaj}
                        onClick={() => {
                            console.log("clicked tecaj ", tecaj.naziv);
                        }}
                    />
                ))}
            </div>
        </div>
    );
}
