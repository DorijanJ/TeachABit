import { useEffect, useState } from "react"
import { AppUserDto } from "../../models/AppUserDto"
import requests from "../../api/agent";
import VerifikacijaZahtjev from "./VerifikacijaZahtjev";

export default function KorisniciAdministracijaPage() {

    const [korisniciVerifikacija, setKorisniciVerifikacija] = useState<AppUserDto[]>([]);

    const GetKorisniciVerifikacijaZahtjev = async () => {
        const response = await requests.getWithLoading("account/verifikacija-zahtjev");

        if (response && response.data) {
            setKorisniciVerifikacija(response.data);
        }
    }

    useEffect(() => {
        GetKorisniciVerifikacijaZahtjev();
    }, [])

    return <>
        <div style={{ display: "flex", flexDirection: "column", gap: "10px" }}>
            {korisniciVerifikacija.map(k => <VerifikacijaZahtjev korisnik={k} />)}
        </div>
    </>
}