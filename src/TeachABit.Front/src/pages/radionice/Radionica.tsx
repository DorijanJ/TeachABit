import { useState, useEffect } from "react";
import { Card, CardContent, Typography, Box, Button } from "@mui/material";
import UserLink from "../profil/UserLink";
import { useNavigate } from "react-router-dom";
import { RadionicaDto } from "../../models/RadionicaDto";
import RadionicaPopup from "./RadionicaPopup";
import globalStore from "../../stores/GlobalStore";
import requests from "../../api/agent";
import { loadStripe } from "@stripe/stripe-js";
import { observer } from "mobx-react";

const stripePromise = loadStripe(import.meta.env.VITE_REACT_STRIPE_KEY);

interface Props {
    radionica: RadionicaDto;
}

export const Radionica = (props: Props) => {
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
            "radionice/create-checkout-session",
            { radionicaId: radionicaId }
        );
        const stripe = await stripePromise;

        const sessionId: any = response?.data.url;

        stripe?.redirectToCheckout({ sessionId });
    };

    const navigate = useNavigate();

  const [isSadrzajOpen, setIsSadrzajOpen] = useState(false);
  const [isLive, setIsLive] = useState(false);

    const [remainingTime, setRemainingTime] = useState({
        dani: 0,
        sati: 0,
        minute: 0,
        sekunde: 0,
    });

    useEffect(() => {
        if (props.radionica.vrijemeRadionice === undefined) return;
        const updateRemainingTime = () => {
            const vrijemeradionice = new Date(
                props.radionica.vrijemeRadionice!
            );
            const sadasnjevrijeme = new Date();
            const razlika =
                vrijemeradionice.getTime() - sadasnjevrijeme.getTime();

            if (razlika > 0) {
                setIsLive(false);
                setRemainingTime({
                    dani: Math.floor(razlika / (1000 * 60 * 60 * 24)),
                    sati: Math.floor(
                        (razlika % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60)
                    ),
                    minute: Math.floor(
                        (razlika % (1000 * 60 * 60)) / (1000 * 60)
                    ),
                    sekunde: Math.floor((razlika % (1000 * 60)) / 1000),
                });
            } else {
                setIsLive(true);
                setRemainingTime({ dani: 0, sati: 0, minute: 0, sekunde: 0 });
            }
        };

        // Postavi interval za ažuriranje svakih 1 sekundu
        const interval = setInterval(updateRemainingTime, 1000);

        // Pokreni odmah prilikom mounta
        updateRemainingTime();

        // Očisti interval kada se komponenta uništi
        return () => clearInterval(interval);
    }, [props.radionica.vrijemeRadionice]);

  return (
    <>
      {isSadrzajOpen &&
        !(globalStore.currentUser?.id === props.radionica.vlasnikId) && (
          <RadionicaPopup
            onClose={() => setIsSadrzajOpen(false)}
            onConfirm={() => handleCheckout(props.radionica.id)}
            radionica={props.radionica}
          />
        )}
      <Card
        id="radionica"
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
          //width: "100%",
          minWidth: "340px",
          maxWidth: "40vw",
        }}
        onClick={() => {
          globalStore.currentUser?.id === props.radionica.vlasnikId ||
          props.radionica.kupljena
            ? navigate(`/radionica/${props.radionica.id}`)
            : setIsSadrzajOpen(true);
        }}
      >
        <CardContent
          sx={{
            textAlign: "center",
            display: "flex",
            flexDirection: "column",
            //justifyContent: "space-between",
            gap: "24px",
            height: "100%",
          }}
        >
          <Box
            display="flex"
            //justifyContent="space-between"
            gap="10px"
            alignItems="flex-start"
          >
            <Typography
              color="primary"
              variant="h5"
              component="div"
              sx={{
                /*overflow: "hidden",
                            textAlign: "left",
                            whiteSpace: "wrap",
                            textOverflow: "ellipsis",
                            maxWidth: "70%",*/
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
                            {props.radionica.naziv}
                        </Typography>

                        <Typography
                            variant="body2"
                            color="textSecondary"
                            sx={{
                                marginLeft: "auto",
                                padding: "4px 8px",
                                backgroundColor: "#f0f0f0",
                                borderRadius: "5px",
                                color: isLive ? "red" : "#254F6A",
                            }}
                        >
                            {isLive
                                ? "UŽIVO"
                                : remainingTime.dani > 0
                                ? `${remainingTime.dani}d ${remainingTime.sati}h `
                                : remainingTime.sati > 0
                                ? `${remainingTime.sati}h ${remainingTime.minute}m `
                                : remainingTime.minute > 0
                                ? `${remainingTime.minute}m ${remainingTime.sekunde}s `
                                : `${remainingTime.sekunde}s `}
                            {/*
              {remainingTime.dani > 0 && `${remainingTime.dani}d `}
              {remainingTime.sati > 0 && `${remainingTime.sati}h `}
              {remainingTime.minute > 0 && `${remainingTime.minute}m `}
              {remainingTime.sekunde > 0 ? `${remainingTime.minute}s ` : "UŽIVO"}
              {remainingTime.dani > 0? */}
                        </Typography>
                    </Box>

                    <div style={{ width: "100%", aspectRatio: "2/1" }}>
                        {props.radionica.naslovnaSlikaVersion ? (
                            <img
                                style={{
                                    borderRadius: "10px",
                                    maxHeight: "100%",
                                    width: "100%",
                                    objectFit: "contain",
                                }}
                                src={`${import.meta.env.VITE_REACT_AWS_BUCKET}${
                                    props.radionica?.naslovnaSlikaVersion
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
                                    display: "flex",
                                    justifyContent: "center",
                                    alignItems: "center",
                                }}
                            >
                                {"Nema slike ¯\\_(ツ)_/¯"}
                            </div>
                        )}
                    </div>

          <Box
            justifyContent={"flex-end"}
            display="flex"
            flexDirection={"row"}
            alignItems="center"
            gap={2}
          >
            <div onClick={(e) => e.stopPropagation()}>
              <UserLink
                user={{
                  id: props.radionica.vlasnikId,
                  username: props.radionica.vlasnikUsername,
                  profilnaSlikaVersion:
                    props.radionica.vlasnikProfilnaSlikaVersion,
                }}
              />
            </div>
            <Box
              display={"flex"}
              alignItems={"flex-end"}
              flexDirection={"row"}
              gap={0.7}
            >
               
                <Button
                  onClick={() => {
                    handleCheckout(props.radionica.id);
                  }}
                  variant="contained"
                >
                  {props.radionica.cijena}€
                </Button>
              
            </Box>
          </Box>
        </CardContent>
      </Card>
    </>
  );
};
export default observer(Radionica);
