import { Box, Button, Card, CardContent, Typography } from "@mui/material";
import { TecajDto } from "../../models/TecajDto";
import { loadStripe } from "@stripe/stripe-js";
import requests from "../../api/agent";
import { useGlobalContext } from "../../context/Global.context";
import globalStore from "../../stores/GlobalStore";
import UserLink from "../profil/UserLink";
import CheckCircleIcon from "@mui/icons-material/CheckCircle";
import { useNavigate } from "react-router-dom";

const stripePromise = loadStripe(import.meta.env.VITE_REACT_STRIPE_KEY);

interface Props {
    tecaj: TecajDto;
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

    const navigate = useNavigate();

    return (
        <Card
            onClick={() => {
                if (props.tecaj.kupljen || !props.tecaj.cijena)
                    navigate(`/tecajevi/${props.tecaj.id}`);
            }}
            sx={{
                borderRadius: "10px",
                boxSizing: "border-box",
                border: "1px solid lightgray",
                cursor: "pointer",
                transition: "transform 0.2s, box-shadow 0.2s",
                "&:hover": {
                    boxShadow: "0px 4px 20px rgba(0, 0, 0, 0.2)",
                    transform: "scale(1.03)",
                    border: "1px solid #3a7ca5",
                },
                height: "370px",
            }}
        >
            <CardContent
                sx={{
                    textAlign: "center",
                    display: "flex",
                    flexDirection: "column",
                    height: "100%",
                    gap: "24px",
                }}
            >
                <div
                    style={{
                        display: "flex",
                        gap: "10px",
                        flexDirection: "column",
                    }}
                >
                    <Box
                        display={"flex"}
                        flexDirection={"row"}
                        justifyContent={"space-between"}
                        alignItems={"flex-start"}
                    >
                        <Typography
                            color="primary"
                            variant="h5"
                            component="div"
                            sx={{
                                textAlign: "left",
                                display: "-webkit-box",
                                WebkitBoxOrient: "vertical",
                                overflow: "hidden",
                                WebkitLineClamp: 3,
                                maxWidth: "100%",
                                height: "6rem",
                                color:
                                    props.tecaj.kupljen === true ||
                                    !props.tecaj.cijena
                                        ? "black"
                                        : "lightgray",
                                marginBottom: "0px",
                            }}
                        >
                            {props.tecaj.naziv}
                        </Typography>
                    </Box>
                    {props.tecaj.naslovnaSlikaVersion ? (
                        <img
                            style={{
                                borderRadius: "10px",
                                objectFit: "cover",
                                height: "170px",
                            }}
                            src={`${import.meta.env.VITE_REACT_AWS_BUCKET}${
                                props.tecaj?.naslovnaSlikaVersion
                            }`}
                        />
                    ) : (
                        <div
                            style={{
                                borderRadius: "10px",
                                objectFit: "cover",
                                width: "100%",
                                height: "170px",
                                backgroundColor: "lightblue",
                            }}
                        />
                    )}
                </div>
                <div
                    style={{
                        display: "flex",
                        width: "100%",
                        justifyContent: "flex-end",
                        gap: "10px",
                        alignItems: "center",
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
                            {props.tecaj.kupljen && (
                                <CheckCircleIcon color="info" />
                            )}
                            <Button
                                disabled={props.tecaj.kupljen}
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
