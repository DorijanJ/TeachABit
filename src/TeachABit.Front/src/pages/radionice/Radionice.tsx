import { Button } from "@mui/material";
import { useGlobalContext } from "../../context/Global.context";
import SearchBox from "../../components/searchbox/SearchBox";
import { useEffect, useState } from "react";
import useRequestBuilder from "../../hooks/useRequestBuilder";
import requests from "../../api/agent";
import { RadionicaDto } from "../../models/RadionicaDto";
import Radionica from "./Radionica";
import RadionicaEditor from "./RadionicaEditor";

// temp function
function abc(query?: string) {
    console.log("Search:", query);
}

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
                <SearchBox onSearch={abc} />

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
                    color: "primary",
                    fontSize: 20,
                    margin: 0,
                    width: "100%",
                }}
            >
                Nadolazeće radionice:
                <hr style={{ border: "1px solid #cccccc" }} />
            </div>

            <div
                style={{
                    display: "flex",
                    flexDirection: "row",
                    flexWrap: "wrap",
                    gap: "20px",
                    width: "100%",
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
