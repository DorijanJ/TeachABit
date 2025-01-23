import { Button } from "@mui/material";
import { useGlobalContext } from "../../context/Global.context";
import SearchBox from "../../components/searchbox/SearchBox";
import { useEffect, useState } from "react";
import useRequestBuilder from "../../hooks/useRequestBuilder";
import requests from "../../api/agent";
import { RadionicaDto } from "../../models/CreateOrUpdateRadionicaDto";
import Radionica from "./Radionica";
import RadionicaEditor from "./RadionicaEditor";

export default function Radionice() {
    const [radionicaList, setRadionicaList] = useState<RadionicaDto[]>([]);
    const [isOpenRadionicaDialog, setIsOpenRadionicaDialog] = useState(false);
    const globalContext = useGlobalContext();
    const { buildRequest } = useRequestBuilder();

    const GetRadionicaList = async (search: string | undefined = undefined) => {
        const response = await requests.getWithLoading(
            buildRequest("Radionice", { search })
        );
        if (response && response.data) setRadionicaList(response.data);
    };

    useEffect(() => {
        GetRadionicaList();
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
                minWidth: "280px",
            }}
        >
            <div
                style={{
                    display: "flex",
                    flexDirection: "row",
                    gap: "20px",
                    width: "100%",
                    flexWrap: "wrap",
                    alignItems: "center",
                }}
            >
                <SearchBox onSearch={GetRadionicaList} />
                {globalContext.userIsLoggedIn && (
                    <Button
                        variant="contained"
                        onClick={() => setIsOpenRadionicaDialog(true)}
                    >
                        Započni novu radionicu
                    </Button>
                )}
            </div>

            <div
                style={{
                    color: "#4f4f4f",
                    fontSize: 20,
                    width: "100%",
                }}
            >
                Nadolazeće radionice:
                <hr style={{ border: "1px solid #cccccc" }} />
            </div>

            <div
                style={{
                    display: "grid",
                    gridTemplateColumns: "repeat(auto-fit, minmax(300px, 1fr))",
                    gap: "20px",
                    maxWidth: "100%",
                    width: "100%",
                    paddingBottom: "20px",
                }}
            >
                {radionicaList.map((radionica) => (
                    <Radionica
                        key={"radionica" + radionica.id}
                        radionica={radionica}
                    />
                ))}
            </div>

            <RadionicaEditor
                refreshData={() => GetRadionicaList()}
                onClose={() => {
                    setIsOpenRadionicaDialog(false);
                }}
                isOpen={isOpenRadionicaDialog}
            />
        </div>
    );
}
