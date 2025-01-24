import {
    Dialog,
    DialogTitle,
    DialogContent,
    DialogActions,
    Button,
    Box,
    Typography,
} from "@mui/material";
import { RadionicaDto } from "../../models/RadionicaDto";
import UserLink from "../profil/UserLink";
import TeachABitRenderer from "../../components/editor/TeachaBitRenderer";
import globalStore from "../../stores/GlobalStore";
import requests from "../../api/agent";
import { loadStripe } from "@stripe/stripe-js";
import { observer } from "mobx-react";
/*import { useEffect } from "react";
import { useParams } from "react-router-dom";*/

const stripePromise = loadStripe(import.meta.env.VITE_REACT_STRIPE_KEY);

interface Props {
    onConfirm: () => Promise<any>;
    onClose: () => void;
    radionica: RadionicaDto;
}

//const { radionicaId } = useParams();

export const RadionicaPopup = (props: Props) => {
    const handleCheckout = async (radionicaId?: number) => {
        if (!globalStore.currentUser) {
            globalStore.addNotification({
                message: "Niste prijavljeni",
                severity: "error",
            });
            return;
        }

        if (!radionicaId) return;
        const response = await requests.postWithLoading(
            "placanja/create-checkout-session",
            { radionicaId: radionicaId }
        );
        const stripe = await stripePromise;

        const sessionId: any = response?.data.url;

        stripe?.redirectToCheckout({ sessionId });
    };

    /*
    const { radionicaId } = useParams();

    useEffect(() => {
        if (radionicaId) {
            getRadionicaById(parseInt(radionicaId));
        }
    }, [radionicaId]);

    const getRadionicaById = async (radionicaId: number) => {
        const response = await requests.getWithLoading(
            `radionice/${radionicaId}`
        );
    };*/

    return (
        <>
            <Dialog
                open
                onClose={props.onClose}
                maxWidth={"md"}
                id="radionicaPopup"
            >
                <DialogTitle sx={{ maxWidth: "100%" }}>
                    <div
                        style={{
                            display: "flex",
                            flexDirection: "row",
                            justifyContent: "space-between",
                            alignItems: "flex-start",
                            width: "100%",
                        }}
                    >
                        <div
                            style={{
                                overflowX: "hidden",
                                whiteSpace: "normal",
                                maxWidth: "80%",
                                //width:"100%",
                                color: "#4f4f4f",
                            }}
                        >
                            {"Podaci o radionici"}
                        </div>
                    </div>
                </DialogTitle>

                <DialogContent
                    sx={{
                        height: 600,
                        width: "100%",
                        display: "flex",
                        flexDirection: "column",
                        gap: "20px",
                        paddingTop: "10px !important",
                        minWidth: "800px",
                    }}
                >
                    {/* Prikaz vlasnika radionice */}
                    <Box
                        flexDirection={"row"}
                        alignItems={"center"}
                        display={"flex"}
                        justifyContent={"space-between"}
                        gap="5px"
                        width={"100%"}
                    >
                        <Box>
                            <Typography
                                color="primary"
                                variant="h5"
                                component="div"
                                sx={{
                                    overflow: "hidden",
                                    whiteSpace: "break-spaces",
                                    maxWidth: "100%",
                                }}
                            >
                                {props.radionica.naziv}
                            </Typography>
                        </Box>

                        <UserLink
                            user={{
                                id: props.radionica?.vlasnikId,
                                username: props.radionica?.vlasnikUsername,
                                profilnaSlikaVersion:
                                    props.radionica
                                        ?.vlasnikProfilnaSlikaVersion,
                            }}
                        />
                    </Box>

                    {props.radionica.naslovnaSlikaVersion && (
                        <div
                            style={{
                                width: "100%",
                                display: "flex",
                                flexDirection: "row",
                                justifyContent: "center",
                                paddingTop: "20px",
                            }}
                        >
                            <img
                                style={{
                                    borderRadius: "10px",
                                    objectFit: "cover",
                                    maxWidth: "100%",
                                    height: "auto",
                                }}
                                src={`${import.meta.env.VITE_REACT_AWS_BUCKET}${
                                    props.radionica.naslovnaSlikaVersion
                                }`}
                            />
                        </div>
                    )}

                    {/* Opis radionice */}
                    <TeachABitRenderer
                        content={props.radionica?.opis || "Nema opisa"}
                    />

                    {/* Kapacitet, cijena i vrijeme */}
                    <div
                        title="kapacitet-cijena-vrijeme-wrapper"
                        style={{
                            //backgroundColor: "lightsteelblue",
                            display: "flex",
                            justifyContent: "space-evenly",
                            alignItems: "center",
                            flexWrap: "wrap",
                            gap: "20px",
                        }}
                    >
                        {/* Kapacitet */}
                        <Box
                            sx={{
                                //backgroundColor: "lightgray",
                                width: "30%",
                                color: "#4f4f4f",
                            }}
                        >
                            Kapacitet:
                            <Typography
                                color="primary"
                                variant="h6"
                                component="div"
                                sx={{
                                    overflow: "hidden",
                                    whiteSpace: "break-spaces",
                                    maxWidth: "90%",
                                }}
                            >
                                {props.radionica.maksimalniKapacitet}
                            </Typography>
                        </Box>

                        {/* Cijena 
            <Box
              sx={{
                //backgroundColor: "lightgray",
                width: "30%",
                color: "#4f4f4f",
              }}
            >
              Cijena:
              <Typography
                color="primary"
                variant="h6"
                component="div"
                sx={{
                  overflow: "hidden",
                  whiteSpace: "break-spaces",
                  maxWidth: "90%",
                }}
              >
                {props.radionica.cijena}
              </Typography>
            </Box>
            */}

                        {/* Vrijeme */}
                        <Box
                            sx={{
                                //backgroundColor: "lightgray",
                                width: "30%",
                                color: "#4f4f4f",
                            }}
                        >
                            Datum i vrijeme radionice:
                            <Typography
                                color="primary"
                                variant="h6"
                                component="div"
                                sx={{
                                    overflow: "hidden",
                                    whiteSpace: "break-spaces",
                                    maxWidth: "90%",
                                }}
                            >
                                {props.radionica?.vrijemeRadionice
                                    ? new Date(
                                          props.radionica.vrijemeRadionice
                                      ).toLocaleString()
                                    : undefined}
                            </Typography>
                        </Box>
                    </div>
                </DialogContent>

                <DialogActions>
                    <Button variant="outlined" onClick={props.onClose}>
                        Odustani
                    </Button>
                    <Button
                        id="confirmButton"
                        variant="contained"
                        onClick={() => handleCheckout(props.radionica.id)}
                    >
                        {props.radionica?.cijena
                            ? ` ${props.radionica.cijena} €`
                            : "Prijavi se"}
                    </Button>
                </DialogActions>
            </Dialog>
        </>
    );
};

export default observer(RadionicaPopup);
