import { Box, Button, Card, CardContent } from "@mui/material";
import { AppUserDto } from "../../models/AppUserDto";
import UserLink from "../profil/UserLink";
import requests from "../../api/agent";
import { VerifikacijaEnum } from "../../enums/VerifikacijaEnum";
import VerifiedIcon from "@mui/icons-material/Verified";

interface Props {
    korisnik: AppUserDto;
    refreshData: () => Promise<any>;
}

export default function VerifikacijaZahtjev(props: Props) {
    const PotvrdiVerifikaciju = async (username: string) => {
        const response = await requests.postWithLoading(
            `account/${username}/verifikacija`
        );
        if (response && response.data) {
            props.refreshData();
        }
    };

    return (
        <Card sx={{ width: "500px", height: "70px" }}>
            <CardContent sx={{ padding: "16px !important" }}>
                <Box
                    display="flex"
                    flexDirection="row"
                    justifyContent="space-between"
                    height={"100%"}
                    alignItems={"center"}
                >
                    <div style={{ width: "180px" }}>
                        <UserLink user={props.korisnik} fontSize={"18px"} />
                    </div>
                    <p
                        style={{
                            margin: "0",
                            height: "100%",
                            display: "flex",
                            alignItems: "center",
                            gap: "10px",
                        }}
                    >
                        {props.korisnik.verifikacijaStatusNaziv}
                        {props.korisnik.verifikacijaStatusNaziv ===
                            "Verificiran" && (
                            <VerifiedIcon
                                sx={{
                                    height: "25px",
                                    width: "25px",
                                    color: "#922728",
                                }}
                            />
                        )}
                    </p>
                    <Box
                        display="flex"
                        flexDirection={"row"}
                        gap="10px"
                        justifyContent={"flex-end"}
                        alignItems={"center"}
                        width={"100px"}
                    >
                        {props.korisnik.verifikacijaStatusId ===
                            VerifikacijaEnum.ZahtjevPoslan && (
                            <Button
                                variant="contained"
                                onClick={() => {
                                    if (!props.korisnik.username) return;
                                    PotvrdiVerifikaciju(
                                        props.korisnik.username
                                    );
                                }}
                            >
                                Odobri
                            </Button>
                        )}
                    </Box>
                </Box>
            </CardContent>
        </Card>
    );
}
