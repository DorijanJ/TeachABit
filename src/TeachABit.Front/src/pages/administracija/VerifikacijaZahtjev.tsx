import { Box, Button, Card, CardContent } from "@mui/material";
import { AppUserDto } from "../../models/AppUserDto";
import UserLink from "../profil/UserLink";

interface Props {
    korisnik: AppUserDto;
}

export default function VerifikacijaZahtjev(props: Props) {
    return <Card sx={{ width: "600px" }}>
        <CardContent>
            <Box display="flex" flexDirection="row" justifyContent="space-between" height={"100%"}>
                <UserLink user={props.korisnik} width={"150px"} fontSize={"18px"} />
                <p>{props.korisnik.verifikacijaStatusNaziv}</p>
                <Box minHeight={"100%"} height={"100%"} display="flex" flexDirection={"row"} gap="10px" alignItems={"center"}>
                    <Button>Odbij</Button>
                    <Button variant="contained">Odobri</Button>
                </Box>
            </Box>
        </CardContent>
    </Card>
}