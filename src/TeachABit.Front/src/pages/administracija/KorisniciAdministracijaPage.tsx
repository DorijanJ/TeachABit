import { useEffect, useState } from "react";
import { AppUserDto } from "../../models/AppUserDto";
import requests from "../../api/agent";
import VerifikacijaZahtjev from "./VerifikacijaZahtjev";
import SearchBox from "../../components/searchbox/SearchBox";
import useRequestBuilder from "../../hooks/useRequestBuilder";
import { Box } from "@mui/material";

export default function KorisniciAdministracijaPage() {
    const [korisniciVerifikacija, setKorisniciVerifikacija] = useState<
        AppUserDto[]
    >([]);
    const { buildRequest } = useRequestBuilder();

    const GetKorisniciVerifikacijaZahtjev = async (
        search?: string | undefined
    ) => {
        const response = await requests.getWithLoading(
            buildRequest("account", { search })
        );

        if (response && response.data) {
            setKorisniciVerifikacija(response.data);
        }
    };

    useEffect(() => {
        GetKorisniciVerifikacijaZahtjev();
    }, []);

    return (
        <Box display="flex" flexDirection={"column"} gap="20px" width="100%">
            <SearchBox onSearch={(s) => GetKorisniciVerifikacijaZahtjev(s)} />
            <div
                style={{
                    display: "flex",
                    flexDirection: "column",
                    gap: "10px",
                }}
            >
                {korisniciVerifikacija.map((k) => (
                    <VerifikacijaZahtjev
                        korisnik={k}
                        refreshData={GetKorisniciVerifikacijaZahtjev}
                    />
                ))}
            </div>
        </Box>
    );
}
