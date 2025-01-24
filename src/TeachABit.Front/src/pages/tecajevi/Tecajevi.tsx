import { useEffect, useState } from "react";
import { TecajDto } from "../../models/TecajDto";
import requests from "../../api/agent";
import { Button } from "@mui/material";
import SearchBox from "../../components/searchbox/SearchBox";
import useRequestBuilder from "../../hooks/useRequestBuilder";
import TecajPopup from "./TecajPopup";
import globalStore from "../../stores/GlobalStore";
import { Tecaj } from "./Tecaj";
import { observer } from "mobx-react";

export const Tecajevi = () => {
    const [tecajList, setTecajList] = useState<TecajDto[]>([]);
    const { buildRequest } = useRequestBuilder();

    const [popupOpen, setDialogOpen] = useState(false);
    const handlePopupOpen = () => setDialogOpen(true);
    const handlePopupClose = () => setDialogOpen(false);

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
                minWidth: "300px",
            }}
        >
            <div
                style={{
                    display: "flex",
                    flexDirection: "row",
                    gap: "20px",
                    alignItems: "center",
                    width: "100%",
                    flexWrap: "wrap",
                }}
            >
                <SearchBox onSearch={GetTecajList} />

                {globalStore.currentUser !== undefined && (
                    <Button
                        variant="contained"
                        onClick={() => {
                            handlePopupOpen();
                        }}
                    >
                        Stvori tečaj
                    </Button>
                )}
                <TecajPopup
                    isOpen={popupOpen}
                    onClose={handlePopupClose}
                    refreshData={() => GetTecajList()}
                />
            </div>
            <div
                style={{
                    color: "#4f4f4f",
                    fontSize: 20,
                    margin: 0,
                    width: "100%",
                }}
            >
                Tečajevi:
                <hr style={{ border: "1px solid #cccccc" }} />
            </div>
            <div
                style={{
                    display: "grid",
                    gridTemplateColumns: "repeat(auto-fit, minmax(350px, 1fr))",
                    gap: "20px",
                    maxWidth: "100%",
                    width: "100%",
                    paddingBottom: "20px",
                }}
            >
                {tecajList.map((tecaj) => (
                    <Tecaj key={"tecaj" + tecaj.id} tecaj={tecaj} />
                ))}
            </div>
        </div>
    );
};

export default observer(Tecajevi);
