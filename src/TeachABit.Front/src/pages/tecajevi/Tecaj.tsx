import {Box, Button, Card, CardContent, IconButton, Typography} from "@mui/material";
import { TecajDto } from "../../models/TecajDto";
import { loadStripe } from "@stripe/stripe-js";
import requests from "../../api/agent";
import globalStore from "../../stores/GlobalStore";
import UserLink from "../profil/UserLink";
import CheckCircleIcon from "@mui/icons-material/CheckCircle";
import { useNavigate } from "react-router-dom";
import { observer } from "mobx-react";
import FavoriteIcon from "@mui/icons-material/Favorite";
import {useState} from "react";

const stripePromise = loadStripe(import.meta.env.VITE_REACT_STRIPE_KEY);

interface Props {
    tecaj: TecajDto;
}

export const Tecaj = (props: Props) => {
    const [isLiked, setIsLiked] = useState(false);
    const handleCheckout = async (tecajId?: number) => {
        if (!globalStore.currentUser) {
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
                if (
                    props.tecaj.kupljen ||
                    !props.tecaj.cijena ||
                    globalStore.currentUser?.id === props.tecaj.vlasnikId
                )
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
                minWidth: "340px",
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
                        alignItems: "center",
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
                            sx={{
                                textAlign: "left",
                                width: "100%",
                                overflowWrap: "break-word",
                                wordBreak: "break-word",
                                display: "-webkit-box",
                                WebkitLineClamp: 3,
                                WebkitBoxOrient: "vertical",
                                overflow: "hidden",
                                height: "6rem",
                            }}
                        >
                            {props.tecaj.naziv}
                        </Typography>
                    </Box>
                    <div style={{ width: "100%", aspectRatio: "2/1" }}>
                        {props.tecaj.naslovnaSlikaVersion ? (
                            <img
                                style={{
                                    borderRadius: "10px",
                                    maxHeight: "100%",
                                    width: "100%",
                                    objectFit: "contain",
                                }}
                                src={`${import.meta.env.VITE_REACT_AWS_BUCKET}${
                                    props.tecaj?.naslovnaSlikaVersion
                                }`}
                                alt="Greška pri učitavanju slike."
                            />
                        ) : (
                            <div
                                style={{
                                    borderRadius: "10px",
                                    objectFit: "cover",
                                    width: "100%",
                                    height: "100%",
                                    backgroundColor: "lightblue",
                                }}
                            />
                        )}
                    </div>
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
                    <IconButton
                        onClick={() => setIsLiked(!isLiked)} // Toggle "liked" state
                        sx={{
                            position: "absolute",
                            top: "10px",
                            left: "10px",
                            backgroundColor: isLiked ? "#f44336" : "white",
                            color: isLiked ? "white" : "#f44336",
                            "&:hover": {
                                backgroundColor: isLiked ? "#d32f2f" : "#fce4ec",
                            },
                        }}
                    >
                        <FavoriteIcon />
                    </IconButton>
                    <div onClick={(e) => e.stopPropagation()}>
                        <UserLink
                            user={{
                                id: props.tecaj.vlasnikId,
                                profilnaSlikaVersion:
                                props.tecaj.vlasnikProfilnaSlikaVersion,
                                username: props.tecaj.vlasnikUsername,
                            }}
                        />
                    </div>
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
};

export default observer(Tecaj);
