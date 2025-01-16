import { Button, Card, CardContent, Typography } from "@mui/material";
import { TecajDto } from "../../models/TecajDto";
import { loadStripe } from "@stripe/stripe-js";
import requests from "../../api/agent";
import { useGlobalContext } from "../../context/Global.context";
import globalStore from "../../stores/GlobalStore";
import UserLink from "../profil/UserLink";

const stripePromise = loadStripe(import.meta.env.VITE_REACT_STRIPE_KEY);

interface Props {
    tecaj: TecajDto;
    onClick: () => void;
}

export default function Tecaj(props: Props) {
    const globalContext = useGlobalContext();

    const handleCheckout = async (tecajId?: number) => {
        if (!globalContext.userIsLoggedIn) {
            globalStore.addNotification({
                message: "Niste prijavljeni",
                severity: "error",
            });
            return;
        }

        if (!tecajId) return;
        const response = await requests.postWithLoading(
            "placanja/create-checkout-session",
            { tecajId: tecajId }
        );
        const stripe = await stripePromise;

        const sessionId: any = response?.data.url;

        stripe?.redirectToCheckout({ sessionId });
    };

    return (
        <Card
            onClick={props.onClick}
            sx={{
                width: "32%",
                height: "400px",
                borderRadius: "10px",
                boxSizing: "border-box",
                border: "1px solid lightgray",
                minWidth: "300px",
            }}
        >
            <CardContent
                sx={{
                    textAlign: "center",
                    display: "flex",
                    flexDirection: "column",
                    justifyContent: "space-between",
                    height: "100%",
                    gap: 1,
                }}
            >
                <Typography
                    color="primary"
                    variant="h5"
                    component="div"
                    sx={{
                        overflow: "hidden",
                        maxWidth: "100%",
                        textWrap: "stable",
                    }}
                >
                    {props.tecaj.naziv}
                </Typography>
                <div>{props.tecaj.opis}</div>
                <div
                    style={{
                        display: "flex",
                        width: "100%",
                        justifyContent: "flex-end",
                        gap: "10px",
                    }}
                >
                    <UserLink
                        user={{
                            id: props.tecaj.vlasnikId,
                            profilnaSlikaVersion:
                                props.tecaj.vlasnikProfilnaSlikaVersion,
                            username: props.tecaj.vlasnikUsername,
                        }}
                    />
                    {props.tecaj.cijena && props.tecaj.cijena > 0 && (
                        <>
                            <Button
                                variant="contained"
                                onClick={() => handleCheckout(props.tecaj.id)}
                            >
                                {props.tecaj.cijena}€
                            </Button>
                        </>
                    )}
                </div>
            </CardContent>
        </Card>
    );
}
