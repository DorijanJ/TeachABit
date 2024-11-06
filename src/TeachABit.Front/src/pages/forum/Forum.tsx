import { useEffect, useState } from "react";
import { ObjavaDto } from "../../models/ObjavaDto";
import requests from "../../api/agent";
import { Box, Button } from "@mui/material";
import Objava from "./Objava";
import { useGlobalContext } from "../../context/Global.context";
import ObjavaDialog from "./ObjavaDialog";

export default function Forum() {
    const [objavaList, setObjavaList] = useState<ObjavaDto[]>([]);
    const [selectedObjava, setSelectedObjava] = useState<ObjavaDto>();
    const globalContext = useGlobalContext();

    const GetObjavaList = async () => {
        const response = await requests.getWithLoading("objave");
        setObjavaList(response.data);
    };

    useEffect(() => {
        GetObjavaList();
    }, []);

    return (
        <Box
            sx={{
                display: "flex",
                flexDirection: "row",
                alignItems: "flex-start",
                height: "100%",
            }}
        >
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
                {globalContext.userIsLoggedIn && (
                    <Button
                        variant="contained"
                        onClick={() =>
                            setSelectedObjava({
                                naziv: "",
                                sadrzaj: "",
                            })
                        }
                    >
                        Stvori objavu
                    </Button>
                )}
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
                            onClick={() => setSelectedObjava(objava)}
                        />
                    ))}
                </div>
            </div>
            {selectedObjava && (
                <ObjavaDialog
                    objava={selectedObjava}
                    setObjava={setSelectedObjava}
                    refreshData={() => GetObjavaList()}
                />
            )}
        </Box>
    );
}
