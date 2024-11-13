import { useEffect, useState } from "react";
import { TecajDto } from "../../models/TecajDto";
import requests from "../../api/agent";
import { Button } from "@mui/material";
import Tecaj from "./Tecaj";
import SearchBox from "../../components/searchbox/SearchBox";
import useRequestBuilder from "../../hooks/useRequestBuilder";

export default function Tecajevi() {

    const [tecajList, setTecajList] = useState<TecajDto[]>([]);

    const { buildRequest } = useRequestBuilder();

    const GetTecajList = async (search: string | undefined = undefined) => {
        const response = await requests.getWithLoading(
            buildRequest("tecajevi", { search })
        );
        setTecajList(response.data);
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
                <SearchBox onSearch={GetTecajList} />
                <Button
                    variant="contained"
                    onClick={() => console.log("stvori tecaj")}
                >
                    Stvori tecaj
                </Button>
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
                {tecajList.map((tecaj) => (
                    <Tecaj
                        key={"tecaj" + tecaj.id}
                        tecaj={tecaj}
                        onClick={() => {
                            console.log("clicked tecaj ", tecaj.naziv)
                        }}
                    />
                ))}
            </div>
            
        </div>
    );
}
