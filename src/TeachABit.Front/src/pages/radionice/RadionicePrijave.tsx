import { Dialog, DialogContent } from "@mui/material";
import { useEffect, useState } from "react";
import { AppUserDto } from "../../models/AppUserDto";
import requests from "../../api/agent";
import UserLink from "../profil/UserLink";

interface Props {
    radionicaId: number;
    kapacitet?: number;
    onClose: () => void;
}

export const RadionicaPrijave = (props: Props) => {
    const [korisnici, setKorisnici] = useState<AppUserDto[]>([]);
    const getPrijave = async (radionicaId: number) => {
        const response = await requests.getWithLoading(
            `radionice/${radionicaId}/prijave`
        );
        if (response && response.data) {
            setKorisnici(response.data);
        }
    };

    useEffect(() => {
        getPrijave(props.radionicaId);
    }, [props.radionicaId]);

    return (
        <Dialog open onClose={props.onClose}>
            <DialogContent
                sx={{
                    width: "600px",
                    height: "600px",
                }}
            >
                {"Broj prijava: "}
                {korisnici.length}
                {props.kapacitet && <>/{props.kapacitet}</>}
                <div
                    style={{
                        marginTop: "10px",
                        display: "flex",
                        flexDirection: "column",
                        overflow: "auto",
                    }}
                >
                    <hr style={{ width: "100%" }} />
                    {korisnici.map((x: AppUserDto) => (
                        <>
                            <UserLink width={"100%"} user={x} />
                            <hr style={{ width: "100%" }} />
                        </>
                    ))}
                </div>
            </DialogContent>
        </Dialog>
    );
};

export default RadionicaPrijave;
