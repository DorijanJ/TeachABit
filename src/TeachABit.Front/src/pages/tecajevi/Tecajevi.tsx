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
import NumberRangeSelector from "./NumberRangeSelector.tsx";
import TecajeviFilter from "./TecajeviFilter.tsx";

interface TecajSearch {
    search?: string | undefined;
    minPrice?: number;
    maxPrice?: number;
    minLikes?: number;
    maxLikes?: number;
    ownerUsername?: string;
}

export const Tecajevi = () => {
    const [tecajList, setTecajList] = useState<TecajDto[]>([]);
    const { buildRequest } = useRequestBuilder();
    const [popupOpen, setPopupOpen] = useState(false);

    const [searchParams, setSearchParams] = useState<TecajSearch>({
        maxLikes: 5,
        minLikes: 1,
        maxPrice: 2000,
        minPrice: 0,
    });

    const GetTecajList = async () => {
        const response = await requests.getWithLoading(
            buildRequest("tecajevi", {
                search: searchParams?.search,
                vlasnikUsername: searchParams?.ownerUsername,
                minCijena: searchParams?.minPrice,
                maxCijena: searchParams?.maxPrice,
                minOcijena: searchParams?.minLikes,
                maxOcijena: searchParams?.maxLikes,
            })
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
                    alignItems: "flex-start",
                    width: "100%",
                    justifyContent: "space-between",
                    flexWrap: "wrap",
                }}
            >
                <div
                    style={{
                        display: "flex",
                        flexDirection: "row",
                        gap: "30px",
                        width: "90%",
                        flexWrap: "wrap",
                    }}
                >
                    <div
                        style={{
                            display: "flex",
                            flexDirection: "column",
                            gap: "10px",
                        }}
                    >
                        <SearchBox
                            onSearch={(s) => {
                                setSearchParams((prev: any) => ({
                                    ...prev,
                                    search: s,
                                }));
                                GetTecajList();
                            }}
                        />
                    </div>
                    <div
                        style={{
                            display: "flex",
                            gap: "50px",
                            alignItems: "flex-start",
                            flexWrap: "wrap",
                        }}
                    >
                        <div style={{ width: "300px" }}>
                            <NumberRangeSelector
                                min={1}
                                max={5}
                                onChangeCommited={(_value: number[]) =>
                                    GetTecajList()
                                }
                                onChange={(value: number[]) => {
                                    setSearchParams((prev: any) => ({
                                        ...prev,
                                        minLikes: value[0],
                                        maxLikes: value[1],
                                    }));
                                }}
                            />

                            <div>
                                Raspon ocjene: {searchParams?.minLikes} -{" "}
                                {searchParams?.maxLikes}
                            </div>
                        </div>

                        <div style={{ width: "300px" }}>
                            <NumberRangeSelector
                                min={0}
                                minDistance={10}
                                onChangeCommited={(_value: number[]) => {
                                    GetTecajList();
                                }}
                                max={2000}
                                onChange={(value: number[]) => {
                                    setSearchParams((prev: any) => ({
                                        ...prev,
                                        minPrice: value[0],
                                        maxPrice: value[1],
                                    }));
                                }}
                            />

                            <div>
                                Raspon cijene: ${searchParams?.minPrice} - $
                                {searchParams?.maxPrice}
                            </div>
                        </div>
                    </div>
                </div>

                {globalStore.currentUser !== undefined && (
                    <Button
                        sx={{ marginTop: "10px" }}
                        variant="contained"
                        onClick={() => {
                            setPopupOpen(true);
                        }}
                    >
                        Stvori tecaj
                    </Button>
                )}

                <TecajPopup
                    isOpen={popupOpen}
                    onClose={() => setPopupOpen(false)}
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
