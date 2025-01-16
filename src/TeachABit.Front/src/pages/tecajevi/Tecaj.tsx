import {
    Box,
    Button,
    Card,
    CardContent,
    IconButton,
    Typography,
} from "@mui/material";
import { TecajDto } from "../../models/TecajDto";
import { loadStripe } from "@stripe/stripe-js";
import requests from "../../api/agent";
import { useGlobalContext } from "../../context/Global.context";
import globalStore from "../../stores/GlobalStore";
import UserLink from "../profil/UserLink";
import CheckCircleIcon from "@mui/icons-material/CheckCircle";
import { useNavigate } from "react-router-dom";
import KeyboardArrowRightIcon from "@mui/icons-material/KeyboardArrowRight";

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

    const navigate = useNavigate();

    return (
        <Card
            onClick={props.onClick}
            sx={{
                width: "32%",
                height: "450px",
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
                <div style={{ display: "flex", gap: "10px", flexDirection: "column" }}>
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
                                overflow: "hidden",
                                textAlign: "left",
                                whiteSpace: "wrap",
                                textOverflow: "ellipsis",
                                maxWidth: "80%",
                            }}
                        >
                            {props.tecaj.naziv}
                        </Typography>
                        {(!props.tecaj.cijena || props.tecaj.kupljen) && (
                            <IconButton
                                color="primary"
                                onClick={() => {
                                    navigate(`/tecajevi/${props.tecaj.id}`);
                                }}
                                size="small"
                                sx={{ border: "1px solid #3a7ca5" }}
                            >
                                <KeyboardArrowRightIcon />
                            </IconButton>
                        )}
                    </Box>
                    {props.tecaj.naslovnaSlikaVersion && (
                        <img
                            style={{
                                objectFit: "cover",
                                width: "100%",
                            }}
                            src={`${import.meta.env
                                .VITE_REACT_AWS_BUCKET
                                }${props.tecaj?.naslovnaSlikaVersion
                                }`}
                        />
                    )}
                    <Typography
                        variant="body1"
                        component="div"
                        sx={{
                            overflow: "hidden",
                            textAlign: "left",
                            whiteSpace: "wrap",
                            maxWidth: "100%",
                            textOverflow: "ellipsis",
                            marginTop: "20px",
                        }}
                    >
                        {props.tecaj.opis}
                    </Typography>
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
