import { useRef, useState, useEffect } from "react";
import { Card, CardContent, Typography, Box, Button } from "@mui/material";
import UserLink from "../profil/UserLink";
import { useNavigate } from "react-router-dom";
import { RadionicaDto } from "../../models/RadionicaDto";

interface Props {
  radionica: RadionicaDto;
}

export default function Radionica(props: Props) {
  const navigate = useNavigate();
  const naslovRef = useRef<HTMLDivElement>(null);
  const opisRef = useRef<HTMLDivElement>(null);
  const cardRef = useRef<HTMLDivElement>(null);
  const [applyFade, setApplyFade] = useState(false);

  const [remainingTime, setRemainingTime] = useState({
    dani: 0,
    sati: 0,
    minute: 0,
    sekunde: 0,
  });

  useEffect(() => {
    if (naslovRef.current && opisRef.current && cardRef.current) {
      const naslovHeight = naslovRef.current.offsetHeight;
      const opisHeight = opisRef.current.offsetHeight;
      const cardHeight = cardRef.current.offsetHeight;
      const totalContentHeight = naslovHeight + opisHeight;
      const contentRatio = totalContentHeight / cardHeight;

      setApplyFade(contentRatio > 0.6); // Fade efekt samo ako sadržaj prelazi 70% visine kartice
    }
  }, [props.radionica.naziv, props.radionica.opis]);

  useEffect(() => {
    const updateRemainingTime = () => {
      const vrijemeradionice = new Date(props.radionica.datumvrijeme);
      const sadasnjevrijeme = new Date();
      const razlika = vrijemeradionice.getTime() - sadasnjevrijeme.getTime();

      if (razlika > 0) {
        setRemainingTime({
          dani: Math.floor(razlika / (1000 * 60 * 60 * 24)),
          sati: Math.floor(
            (razlika % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60)
          ),
          minute: Math.floor((razlika % (1000 * 60 * 60)) / (1000 * 60)),
          sekunde: Math.floor((razlika % (1000 * 60)) / 1000),
        });
      } else {
        setRemainingTime({ dani: 0, sati: 0, minute: 0, sekunde: 0 });
      }
    };

    // Postavi interval za ažuriranje svakih 1 sekundu
    const interval = setInterval(updateRemainingTime, 1000);

    // Pokreni odmah prilikom mounta
    updateRemainingTime();

    // Očisti interval kada se komponenta uništi
    return () => clearInterval(interval);
  }, [props.radionica.datumvrijeme]);

  return (
    <Card
      id="radionica"
      ref={cardRef}
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
        height: "360px",
      }}
      onClick={() => {
        navigate(`/radionica/${props.radionica.id}`);
      }}
    >
      <CardContent
        sx={{
          textAlign: "center",
          display: "flex",
          flexDirection: "column",
          justifyContent: "space-between",
          gap: 1,
          height: "100%",
        }}
      >
        <Box
          display="flex"
          justifyContent="space-between"
          alignItems="flex-start"
        >
          <Typography
            ref={naslovRef}
            color="primary"
            variant="h5"
            component="div"
            sx={{
              overflow: "hidden",
              textAlign: "left",
              whiteSpace: "wrap",
              textOverflow: "ellipsis",
              maxWidth: "70%",
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
            }}
          >
            {remainingTime.dani > 0 && `${remainingTime.dani}d `}
            {remainingTime.sati > 0 && `${remainingTime.sati}h `}
            {remainingTime.minute > 0 && `${remainingTime.minute}m `}
            {remainingTime.sekunde}s
          </Typography>
        </Box>

        <Typography
          ref={opisRef}
          variant="body1"
          component="div"
          sx={{
            position: "relative",
            overflow: "hidden",
            textAlign: "left",
            maxHeight: applyFade ? "6rem" : "none", // Dinamično postavljanje visine
            paddingRight: "1rem",
            marginBottom: "1rem",
            "&::after": applyFade
              ? {
                  content: '""',
                  position: "absolute",
                  bottom: 0,
                  left: 0,
                  width: "100%",
                  height: "3rem", // Fade efekt
                  background:
                    "linear-gradient(to bottom, rgba(255, 255, 255, 0), rgba(255, 255, 255, 1))",
                }
              : undefined,
          }}
        >
          {props.radionica.opis}
        </Typography>

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
            {props.radionica.cijena && props.radionica.cijena > 0 && (
              <Button disabled variant="contained">
                {props.radionica.cijena}€
              </Button>
            )}
          </Box>
        </Box>
      </CardContent>
    </Card>
  );
}
