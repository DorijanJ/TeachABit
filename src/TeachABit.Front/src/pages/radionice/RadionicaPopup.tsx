import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  Box,
  Typography,
} from "@mui/material";
import { CreateOrUpdateRadionicaDto } from "../../models/CreateOrUpdateRadionicaDto";
import UserLink from "../profil/UserLink";
import TeachABitRenderer from "../../components/editor/TeachaBitRenderer";
/*import requests from "../../api/agent";
import { useEffect } from "react";
import { useParams } from "react-router-dom";*/

interface Props {
  onConfirm: () => Promise<any>;
  onClose: () => void;
  radionica: CreateOrUpdateRadionicaDto;
}

//const { radionicaId } = useParams();

export default function RadionicaPopup(props: Props) {
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
      <Dialog open onClose={props.onClose} maxWidth={"md"} id="radionicaPopup">
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
              }}
            >
              {"Podaci o radionici"}
            </div>
          </div>
        </DialogTitle>

        <DialogContent
          sx={{
            height: 600,
            width: 900,
            display: "flex",
            flexDirection: "column",
            gap: "20px",
            paddingTop: "10px !important",
          }}
        >
          {/* Prikaz vlasnika radionice */}
          <Box
            flexDirection={"row"}
            alignItems={"center"}
            display={"flex"}
            justifyContent={"space-between"}
            gap="5px"
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
                  props.radionica?.vlasnikProfilnaSlikaVersion,
              }}
            />
          </Box>

          {/* Opis radionice */}
          <TeachABitRenderer content={props.radionica?.opis || "Nema opisa"} />

          {/* Kapacitet, cijena i vrijeme */}
          <div
            title="kapacitet-cijena-vrijeme-wrapper"
            style={{
                //backgroundColor: "lightsteelblue",
              display: "flex",
              justifyContent: "space-between",
              alignItems: "center",
              flexWrap: "wrap",
              gap: "20px",
            }}
          >
            {/* Kapacitet */}
            <Box sx={{ 
                //backgroundColor: "lightgray",
                width: "30%" }}>
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

            {/* Cijena */}
            <Box sx={{ //backgroundColor: "lightgray", 
                width: "30%" }}>
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

            {/* Vrijeme */}
            <Box sx={{ //backgroundColor: "lightgray", 
                width: "30%" }}>
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
                ? new Date(props.radionica.vrijemeRadionice).toLocaleString()
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
            onClick={props.onConfirm}
          >
            {props.radionica?.cijena
              ? ` ${props.radionica.cijena} €`
              : "Prijavi se"}
          </Button>
        </DialogActions>
      </Dialog>
    </>
  );
}
