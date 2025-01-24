import { useEffect, useState } from "react";
import { AppUserDto } from "../../models/AppUserDto";
import requests from "../../api/agent";
import useRequestBuilder from "../../hooks/useRequestBuilder";
import { DataGrid, GridColDef, GridRenderCellParams } from "@mui/x-data-grid";
import { Box, Button, Paper } from "@mui/material";
import SearchBox from "../../components/searchbox/SearchBox";
import Uloga from "../../models/Uloga";
import UserLink from "../profil/UserLink";
import { VerifikacijaEnum } from "../../enums/VerifikacijaEnum";
import VerifiedIcon from "@mui/icons-material/Verified";
import { KorisnikStatus } from "../../enums/KorisnikStatus";
import MicOffIcon from "@mui/icons-material/MicOff";

export default function KorisniciAdministracijaPage() {
    const [korisnici, setKorisnici] = useState<AppUserDto[]>([]);
    const { buildRequest } = useRequestBuilder();

    const GetKorisnici = async (search?: string | undefined) => {
        const response = await requests.getWithLoading(
            buildRequest("account", { search })
        );

        if (response && response.data) {
            setKorisnici(response.data);
        }
    };

    const PotvrdiVerifikaciju = async (username: string) => {
        const response = await requests.postWithLoading(
            `account/${username}/verifikacija`
        );
        if (response && response.data) {
            GetKorisnici();
        }
    };

    useEffect(() => {
        GetKorisnici();
    }, []);

    const columns: GridColDef[] = [
        {
            width: 200,
            headerClassName: "header-padded",
            field: "username",
            headerName: "Ime",
            type: "string",
            align: "center",
            renderCell: (params: GridRenderCellParams<any, AppUserDto>) =>
                params.row ? (
                    <UserLink
                        fontSize={18}
                        width={"100%"}
                        height={"100%"}
                        user={params.row}
                    />
                ) : (
                    ""
                ),
        },
        {
            width: 120,
            field: "verifikacijaStatusId",
            headerName: "Verificiran",
            headerAlign: "center",
            align: "center",
            display: "flex",
            renderCell: (params: GridRenderCellParams<any, number>) =>
                params.value === VerifikacijaEnum.Verificiran ? (
                    <VerifiedIcon color="primary" />
                ) : (
                    params.value === VerifikacijaEnum.ZahtjevPoslan && (
                        <Button
                            onClick={() =>
                                PotvrdiVerifikaciju(params.row.username)
                            }
                            variant="contained"
                        >
                            {"Odobri"}
                        </Button>
                    )
                ),
        },
        {
            width: 100,
            field: "korisnikStatusId",
            headerAlign: "center",
            headerName: "Status",
            align: "left",
            display: "flex",
            renderCell: (params: GridRenderCellParams<any, number>) =>
                params.value === KorisnikStatus.Utisan ? (
                    <MicOffIcon color="primary" />
                ) : (
                    ""
                ),
        },
        {
            flex: 1,
            field: "roles",
            headerName: "Uloge",
            type: "string",
            valueGetter: (e: Uloga[] | undefined) =>
                e !== undefined && e.length > 0 ? e[0].name : "",
        },
    ];

    return (
        <Box display="flex" gap="20px" flexDirection={"column"} height={"100%"}>
            <SearchBox width={"600px"} onSearch={(s) => GetKorisnici(s)} />
            <div style={{ height: "100%" }}>
                <Paper
                    sx={{
                        position: "relative",
                        width: "600px",
                        height: "100%",
                    }}
                >
                    <DataGrid
                        rows={korisnici}
                        columns={columns}
                        rowHeight={60}
                        disableColumnResize
                        sx={{
                            position: "absolute",
                            height: "100%",
                            width: "100%",
                        }}
                    />
                </Paper>
            </div>
        </Box>
    );
}
