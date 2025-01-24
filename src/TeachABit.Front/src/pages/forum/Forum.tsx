import { useEffect, useState } from "react";
import { ObjavaDto } from "../../models/ObjavaDto";
import requests from "../../api/agent";
import { Button } from "@mui/material";
import Objava from "./Objava";
import SearchBox from "../../components/searchbox/SearchBox";
import useRequestBuilder from "../../hooks/useRequestBuilder";
import ObjavaEditor from "./ObjavaEditor";
import { observer } from "mobx-react";
import globalStore from "../../stores/GlobalStore";

export const Forum = () => {
    const [objavaList, setObjavaList] = useState<ObjavaDto[]>([]);
    const [isOpenObjavaDialog, setIsOpenObjavaDialog] = useState(false);

    const { buildRequest } = useRequestBuilder();

    const GetObjavaList = async (search: string | undefined = undefined) => {
        const response = await requests.getWithLoading(
            buildRequest("objave", { search })
        );
        if (response && response.data) setObjavaList(response.data);
    };

    useEffect(() => {
        GetObjavaList();
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
                    width: "100%",
                    flexWrap: "wrap",
                }}
            >
                <SearchBox onSearch={GetObjavaList} />
                {globalStore.currentUser !== undefined &&
                    !globalStore.isMuted() && ( //ako korisnik nije prijavljen, ne može kreirati objave
                        <Button
                            variant="contained"
                            onClick={() => setIsOpenObjavaDialog(true)}
                        >
                            {"Stvori objavu"}
                        </Button>
                    )}
            </div>

            <div
                style={{
                    display: "flex",
                    flexDirection: "column",
                    gap: "20px",
                    maxWidth: "100%",
                    width: "100%",
                }}
            >
                {objavaList.map((objava) => (
                    <Objava key={"objava" + objava.id} objava={objava} />
                ))}
            </div>

            <ObjavaEditor
                refreshData={() => GetObjavaList()}
                onClose={() => {
                    setIsOpenObjavaDialog(false);
                }}
                isOpen={isOpenObjavaDialog}
            />
        </div>
    );
};

export default observer(Forum);
