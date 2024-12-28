import { useEffect, useState } from "react";
import { ObjavaDto } from "../../models/ObjavaDto";
import requests from "../../api/agent";
import { Button } from "@mui/material";
import Objava from "./Objava";
import { useGlobalContext } from "../../context/Global.context";
import SearchBox from "../../components/searchbox/SearchBox";
import useRequestBuilder from "../../hooks/useRequestBuilder";
import { useNavigate } from "react-router-dom";
import CreateObjavaDialog from "./CreateObjavaDialog";

export default function Forum() {
    const [objavaList, setObjavaList] = useState<ObjavaDto[]>([]);
    const [isOpenObjavaDialog, setIsOpenObjavaDialog] = useState(false);
    const globalContext = useGlobalContext();
    const navigate = useNavigate();

    const { buildRequest } = useRequestBuilder();

    const GetObjavaList = async (search: string | undefined = undefined) => {
        const response = await requests.getWithLoading(
            buildRequest("objave", { search })
        );
        if (response.data) setObjavaList(response.data);
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
                alignItems: "flex-start",
                height: "100%",
                width: "100%",
            }}
        >
            <div
                style={{
                    display: "flex",
                    flexDirection: "row",
                    gap: "20px",
                    width: "100%",
                    alignItems: "center",
                }}
            >
                <SearchBox onSearch={GetObjavaList} />
                {globalContext.userIsLoggedIn && ( //ako korisnik nije prijavljen, ne može kreirati objave
                    <Button
                        variant="contained"
                        onClick={() => setIsOpenObjavaDialog(true)}
                    >
                        Stvori objavu
                    </Button>
                )}
            </div>

            <div
                style={{
                    display: "flex",
                    flexDirection: "row",
                    flexWrap: "wrap",
                    gap: "20px",
                    maxWidth: "100%",
                }}
            >
                {objavaList.map((objava) => (
                    <Objava
                        key={"objava" + objava.id}
                        objava={objava}
                        onClick={() => {
                            navigate(`/objava/${objava.id}`);
                        }}
                    />
                ))}
            </div>

            <CreateObjavaDialog
                refreshData={() => GetObjavaList()}
                onClose={() => {
                    setIsOpenObjavaDialog(false);
                }}
                isOpen={isOpenObjavaDialog}
            />
        </div>
    );
}
