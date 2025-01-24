import {useEffect, useState} from "react";
import {TecajDto} from "../../models/TecajDto";
import requests from "../../api/agent";
import {Button, Menu, MenuItem} from "@mui/material";
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
    const [reversedOrder, setReversedOrder] = useState<boolean>(false);

    const [priceRange, setPriceRange] = useState<[number, number]>([0, 100]);
    const [PriceRangeOpen, setPriceRangeOpen] = useState(false);

    const [LikesRangeOpen, setLikesRangeOpen] = useState(false);
    const [likesRange, setLikesRange] = useState<[number, number]>([0, 100]);


    const [OwnerSelectorOpen, setOwnerSelectorOpen] = useState(false);
    const [owner, setOwner] = useState<string | undefined>(undefined);

    const handlePopupOpen = () => setDialogOpen(true);
    const handlePopupClose = () => setDialogOpen(false);

    const handlePriceRangePopupOpen = () => setPriceRangeOpen(true);
    const handlePriceRangePopupClose = () => setPriceRangeOpen(false);

    const handleNewestFirst =() => {
        if(reversedOrder){
            setTecajList(reverseList(tecajList))
            setReversedOrder(false)
        }
    };
    const handleOldestFirst =() => {
        if(!reversedOrder){
            setTecajList(reverseList(tecajList))
            setReversedOrder(true)
        }
    };


    const handleOwnerPopupOpen = () => setOwnerSelectorOpen(true);
    const handleOwnerPopupClose = () => setOwnerSelectorOpen(false);

    const handleLikesPopupOpen = () => setLikesRangeOpen(true);
    const handleLikesPopupClose = () => setLikesRangeOpen(false);



    const GetTecajList = async (
        search: string | undefined = undefined,
        minPrice?: string,
        maxPrice?: string,
        minLikes?: string,
        maxLikes?: string,
        ownerUsername?: string
    ) => {
        const response = await requests.getWithLoading(
            buildRequest("tecajevi", {
                search,
                minPrice,
                maxPrice,
                minLikes,
                maxLikes,
                ownerUsername,
            })
        );
        if (response && response.data) setTecajList(response.data);
    };

    const reverseList = (list: TecajDto[]): TecajDto[] => {
        return [...list].reverse(); // Create a copy and reverse
    };


    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
    const open = Boolean(anchorEl);
    const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
        setAnchorEl(event.currentTarget);
    };
    const handleClose = () => {
        setAnchorEl(null);
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
                    onClick={handleClick}
                >
                    Filtriraj
                </Button>
                <Menu
                    id="basic-menu"
                    anchorEl={anchorEl} // Make sure anchorEl refers to the button element
                    open={open}
                    anchorOrigin={{
                        vertical: "bottom", // Anchor to the bottom of the button
                        horizontal: "center", // Center horizontally relative to the button
                    }}
                    transformOrigin={{
                        vertical: "top", // Start the menu from the top of itself
                        horizontal: "center", // Align horizontally with the button
                    }}
                    onClose={handleClose}
                    MenuListProps={{
                        "aria-labelledby": "basic-button",
                    }}
                >
                    <MenuItem
                        sx={{width: "220px"}}
                        onClick={() => {
                            handleClose();
                            handlePriceRangePopupOpen();
                        }}
                    >
                        <div style={{display: "flex", gap: "10px"}}>Cijena</div>
                    </MenuItem>
                    <MenuItem
                        onClick={() => {
                            handleClose();
                            handleOwnerPopupOpen();
                        }}
                    >
                        <div style={{display: "flex", gap: "10px"}}>Po vlasniku</div>
                    </MenuItem>
                    <MenuItem
                        onClick={() => {
                            handleClose();
                            handleLikesPopupOpen();
                        }}
                    >
                        <div style={{display: "flex", gap: "10px"}}>Po ocjeni</div>
                    </MenuItem>
                    <MenuItem
                        onClick={() => {
                            handleClose();
                            handleNewestFirst();
                        }}
                    >
                        <div style={{display: "flex", gap: "10px"}}>Najnovije</div>
                    </MenuItem>
                    <MenuItem
                        onClick={() => {
                            handleClose();
                            handleOldestFirst();
                        }}
                    >
                        <div style={{display: "flex", gap: "10px"}}>Najstarije</div>
                    </MenuItem>

                </Menu>


                {PriceRangeOpen && (
                    <div style={{display: "flex", justifyContent: "space-between", alignItems: "center"}}>
                        <div>
                            <PriceRangeSelector
                                min={0}
                                max={100}
                                onChange={(value: [number, number]) => {
                                    setPriceRange(value); // Update state with the new range
                                    GetTecajList(
                                        undefined,
                                        value[0].toString(),
                                        value[1].toString(), // Apply new price filter
                                        likesRange[0].toString(),
                                        likesRange[1].toString(),
                                        owner
                                    );
                                }}
                            />

                            <p>Raspon cijene: ${priceRange[0]} - ${priceRange[1]}</p>
                        </div>
                        <IconButton
                            onClick={() => {
                                setPriceRange([0, 100]); // Reset price range
                                handlePriceRangePopupClose();

                                // Reapply remaining filters
                                GetTecajList(
                                    undefined,
                                    undefined,
                                    undefined, // Reset price filters
                                    likesRange[0].toString(),
                                    likesRange[1].toString(),
                                    owner
                                );
                            }}
                        >
                            ✖
                        </IconButton>

                    </div>

                )}
                {LikesRangeOpen && (
                    <div style={{display: "flex", justifyContent: "space-between", alignItems: "center"}}>
                        <div>
                            <PriceRangeSelector
                                min={1}
                                max={5}
                                onChange={(value: [number, number]) => {
                                    setLikesRange(value); // Update state with the new range
                                    GetTecajList(
                                        undefined,
                                        priceRange[0].toString(),
                                        priceRange[1].toString(),
                                        value[0].toString(),
                                        value[1].toString(), // Apply new likes filter
                                        owner
                                    );
                                }}
                            />

                            <p>Raspon ocjene: {priceRange[0]} - {priceRange[1]}</p>
                        </div>
                        <IconButton
                            onClick={() => {
                                setLikesRange([1, 5]); // Reset likes range
                                handleLikesPopupClose();

                                // Reapply remaining filters
                                GetTecajList(
                                    undefined,
                                    priceRange[0].toString(),
                                    priceRange[1].toString(),
                                    undefined, // Reset likes filters
                                    undefined,
                                    owner
                                );
                            }}
                        >
                            ✖
                        </IconButton>

                    </div>
                )}
                {OwnerSelectorOpen && (
                    <div style={{display: "flex", justifyContent: "space-between", alignItems: "center"}}>
                        <div>
                            <SearchBox
                                onSearch={(text) => {
                                    setOwner(text); // Update state with the selected owner
                                    GetTecajList(
                                        undefined,
                                        priceRange[0].toString(),
                                        priceRange[1].toString(),
                                        likesRange[0].toString(),
                                        likesRange[1].toString(),
                                        text // Apply owner filter
                                    );
                                }}
                                height="3rem"
                                width="5rem"
                            />

                        </div>
                        <IconButton
                            onClick={() => {
                                setOwner(undefined); // Reset owner filter
                                handleOwnerPopupClose();

                                // Reapply remaining filters
                                GetTecajList(
                                    undefined,
                                    priceRange[0].toString(),
                                    priceRange[1].toString(),
                                    likesRange[0].toString(),
                                    likesRange[1].toString(),
                                    undefined // Reset owner filter
                                );
                            }}
                        >
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
