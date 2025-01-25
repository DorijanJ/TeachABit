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
        minLikes: 0,
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
                    alignItems: "center",
                    width: "100%",
                    flexWrap: "wrap",
                    maxWidth: "100%",
                }}
            >
                <SearchBox
                    width={"auto"}
                    onSearch={(s) => {
                        setSearchParams((prev: any) => ({
                            ...prev,
                            search: s,
                        }));
                        GetTecajList();
                    }}
                />
                {globalStore.currentUser !== undefined && (
                    <Button
                        variant="contained"
                        onClick={() => {
                            setPopupOpen(true);
                        }}
                    >
                        Stvori tecaj
                    </Button>
                )}
            </div>
            <div
                style={{
                    display: "flex",
                    alignItems: "flex-start",
                    flexWrap: "wrap",
                    maxWidth: "100%",
                }}
            >
                <div style={{ width: "280px", marginRight: "50px" }}>
                    <NumberRangeSelector
                        min={0}
                        max={5}
                        onChangeCommited={(_value: number[]) => GetTecajList()}
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

                <div style={{ width: "280px" }}>
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

                {popupOpen && (
                    <TecajPopup
                        isOpen={popupOpen}
                        onClose={() => setPopupOpen(false)}
                        refreshData={() => GetTecajList()}
                    />
                )}

                <div
                    style={{
                        color: "#4f4f4f",
                        fontSize: 20,
                        margin: "10px 0",
                        width: "100%",
                    }}
                >
                    Tečajevi:
                    <hr style={{ border: "1px solid #cccccc" }} />
                </div>
                <div
                    style={{
                        display: "flex",
                        flexWrap: "wrap",
                        gap: "20px",
                        paddingBottom: "20px",
                        maxWidth: "100%",
                    }}
                >
                    {tecajList.map((tecaj) => (
                        <Tecaj key={"tecaj" + tecaj.id} tecaj={tecaj} />
                    ))}
                </div>
            </div>
        </div>
    );
};

export default observer(Tecajevi);
