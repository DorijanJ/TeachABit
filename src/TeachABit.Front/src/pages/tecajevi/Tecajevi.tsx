import {useEffect, useState} from "react";
import {TecajDto} from "../../models/TecajDto";
import requests from "../../api/agent";
import {Button} from "@mui/material";
import {useGlobalContext} from "../../context/Global.context";
import Tecaj from "./Tecaj";
import SearchBox from "../../components/searchbox/SearchBox";
import useRequestBuilder from "../../hooks/useRequestBuilder";
import TecajPopup from "./TecajPopup";
import PriceRangeSelector from "./PriceRangeSelector.tsx";
import {IconButton} from "@mui/material";


export default function Tecajevi() {
    const [tecajList, setTecajList] = useState<TecajDto[]>([]);
    const globalContext = useGlobalContext();
    const {buildRequest} = useRequestBuilder();


    const [popupOpen, setDialogOpen] = useState(false);
    const [priceRange, setPriceRange] = useState<[number, number]>([0, 100]);
    const [PriceRangeOpen, setPriceRangeOpen] = useState(false);


    const handlePopupOpen = () => setDialogOpen(true);
    const handlePopupClose = () => setDialogOpen(false);

    const handlePriceRangePopupOpen = () => setPriceRangeOpen(true);
    const handlePriceRangePopupClose = () => setPriceRangeOpen(false);

    const getIntersection = <T, >(list1: T[], list2: T[]): T[] => {
        return list1.filter(item => list2.includes(item));
    };

    const GetTecajListFilter = async (minCijena: string | undefined = undefined, maxCijena: string | undefined = undefined) => {
        const responseMin = await requests.getWithLoading(
            buildRequest("tecajevi", {minCijena})
        );
        const responseMax = await requests.getWithLoading(
            buildRequest("tecajevi", {maxCijena})
        );

        if (responseMin && responseMin.data && responseMax && responseMax.data) setTecajList(getIntersection(responseMax.data, responseMin.data));
        else if (responseMin && responseMin.data) setTecajList(responseMin.data);
        else if (responseMax && responseMax.data) setTecajList(responseMax.data);
    };


    const GetTecajList = async (search: string | undefined = undefined) => {
        const response = await requests.getWithLoading(
            buildRequest("tecajevi", {search})
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
                <SearchBox onSearch={GetTecajList}/>

                <Button
                    variant="contained"
                    onClick={() => {
                        handlePriceRangePopupOpen();
                    }}
                >
                    Filtriraj
                </Button>

                {PriceRangeOpen && (
                    <div>
                        <PriceRangeSelector
                            min={0}
                            max={100}
                            onChange={(value: [number, number]) => {
                                setPriceRange(value);
                                GetTecajListFilter(value[0].toString(), value[1].toString());
                            }}
                        />
                        <p>Raspon cijene: ${priceRange[0]} - ${priceRange[1]}</p>
                        <IconButton onClick={handlePriceRangePopupClose}>
                            ✖
                        </IconButton>
                    </div>
                )}
            {globalContext.userIsLoggedIn && (
                <Button
                    variant="contained"
                    onClick={() => {
                        handlePopupOpen();
                    }}
                >
                    Stvori tecaj
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
        <hr style={{border: "1px solid #cccccc"}}/>
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
            <Tecaj key={"tecaj" + tecaj.id} tecaj={tecaj}/>
        ))}
    </div>
</div>
)
    ;
}
