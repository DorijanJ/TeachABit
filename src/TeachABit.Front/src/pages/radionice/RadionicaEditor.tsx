import {
  Dialog,
  DialogTitle,
  DialogContent,
  TextField,
  DialogActions,
  Button,
  InputAdornment,
  Box,
} from "@mui/material";
import { useState, ChangeEvent } from "react";
import requests from "../../api/agent";
import { RadionicaDto } from "../../models/RadionicaDto";
import { UpdateRadionicaDto } from "../../models/UpdateRadionicaDto";
import { DateTimePicker } from "@mui/x-date-pickers/DateTimePicker";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider/LocalizationProvider";
import { renderTimeViewClock } from "@mui/x-date-pickers/timeViewRenderers";
import { Today } from "@mui/icons-material";
import dayjs, { Dayjs } from "dayjs";
import { isToday } from "date-fns";

interface Props {
  refreshData: () => Promise<any>;
  onClose: () => void;
  isOpen: boolean;
  radionica?: RadionicaDto;
}

export default function RadionicaEditor(props: Props) {
  const [radionica, setRadionica] = useState<RadionicaDto>({
    naziv: props.radionica?.naziv ?? "",
    /*{sadrzaj: props.radionica?.sadrzaj ?? "",}*/
    id: props.radionica?.id,
    opis: props.radionica?.opis ?? "",
    vlasnikId: props.radionica?.vlasnikId,
    vlasnikUsername: props.radionica?.vlasnikUsername,
    vlasnikProfilnaSlikaVersion: props.radionica?.vlasnikProfilnaSlikaVersion,
    brojprijavljenih: props.radionica?.brojprijavljenih ?? 0,
    kapacitet: props.radionica?.kapacitet,
    datumvrijeme: props.radionica?.datumvrijeme ?? new Date(),
    cijena: props.radionica?.cijena ?? 9.99,
  });

  const handleClose = (reload: boolean = false) => {
    setRadionica({
      naziv: "",
      opis: "",
      cijena: 9.99,
      datumvrijeme: new Date(),
    });
    props.onClose();
    if (reload) props.refreshData();
  };

  const handleUpdateRadionicu = async (radionica: RadionicaDto) => {
    const updateRadionicaDto: UpdateRadionicaDto = {
      id: radionica.id,
      naziv: radionica.naziv,
      opis: props.radionica?.opis ?? "",
      /*predavacProfilnaSlika: props.radionica?.predavacProfilnaSlika,*/
      //brojprijavljenih: props.radionica?.brojprijavljenih ?? 0,
      cijena: props.radionica?.cijena,
      kapacitet: props.radionica?.kapacitet,
      datumvrijeme: props.radionica?.datumvrijeme ?? new Date(),
    };
    const response = await requests.putWithLoading(
      "radionice",
      updateRadionicaDto
    );
    if (response && response.data) {
      handleClose(true);
    }
  };

  const handleStvoriRadionicu = async (radionica: RadionicaDto) => {
    const response = await requests.postWithLoading("radionice", radionica);
    if (response && response.data) {
      handleClose(true);
    }
  };

  return (
    <>
      <Dialog
        open={props.isOpen}
        onClose={() => {
          handleClose();
        }}
        maxWidth={"md"}
        id="radionicaEditor"
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
              }}
            >
              {`Nova radionica`}
            </div>
          </div>
        </DialogTitle>

        <DialogContent
          sx={{
            height: 600,
            display: "flex",
            flexDirection: "column",
            gap: "20px",
            paddingTop: "10px !important",
            maxHeight: "70vh",
            minWidth: "50vw",
          }}

          /*
                  bilo bi dobro tu imat nekakvu sliku al je nema trenutno
                */
        >
          <TextField
            fullWidth
            autoFocus
            label="Naziv radionice"
            name="naziv"
            variant="outlined"
            //required = {true}
            value={radionica.naziv || ""}
            onChange={(e: ChangeEvent<HTMLInputElement>) =>
              setRadionica((prev: any) => ({
                ...prev,
                naziv: e.target.value,
              }))
            }
          />

          <Box
          sx={{
            border: "blue 1px dotted"
          }}
          >Slika neka</Box>

          <TextField
            fullWidth
            autoFocus
            label="Opis radionice"
            name="opis"
            variant="outlined"
            multiline
            rows={2}
            //required = {true}
            value={radionica.opis || ""}
            onChange={(e: ChangeEvent<HTMLInputElement>) =>
              setRadionica((prev: any) => ({
                ...prev,
                opis: e.target.value,
              }))
            }
          />

          <div 
          title = "kapacitet-cijena-vrijeme-wrapper"
          style={{
            display: "flex",
            justifyContent: "space-between",
            alignItems: "center", // Centriranje po okomitoj osi
            flexWrap: "wrap", // Ako nema dovoljno prostora, elementi prelaze u novi red
            gap: "20px", // Razmak između elemenata
          }}
          >

            <TextField
              autoFocus
              label="Kapacitet"
              variant="outlined"
              sx={{
                width: "30%",
              }}
              value={radionica.kapacitet || ""}
              onChange={(e: ChangeEvent<HTMLInputElement>) => {
                const numericValue = e.target.value.replace(/[^0-9]/g, "");
                setRadionica((prev: any) => ({
                  ...prev,
                  kapacitet: numericValue,
                }));
              }}
            />

            <TextField
              autoFocus
              label="Cijena"
              name="cijena"
              sx={{
                width: "30%",
              }}
              variant="outlined"
              value={radionica.cijena?.toString() || ""}
              //type="number"
              slotProps={{
                input: {
                  startAdornment: (
                    <InputAdornment position="start">€</InputAdornment>
                  ),
                },
              }}
              onChange={(e: ChangeEvent<HTMLInputElement>) => {
                const value = e.target.value.replace(/[^0-9,.]/g, "");
                /*const decimalPlaces = value.toString().split(".")[1]?.length;
                if (!(Math.floor(value) === value) && decimalPlaces > 2) {
                  return;
                }*/
                setRadionica((prev: any) => ({
                  ...prev,
                  cijena: value,
                }));
              }}
            />

            <LocalizationProvider dateAdapter={AdapterDayjs}>
              <DateTimePicker
                label="Datum i vrijeme"
                sx={{
                  width: "30%",
                }}
                ampm={false}
                disablePast={true}
                viewRenderers={{
                  hours: renderTimeViewClock,
                  minutes: renderTimeViewClock,
                  seconds: renderTimeViewClock,
                }}
              />
            </LocalizationProvider>
          </div>

        </DialogContent>

        <DialogActions>
          <Button variant="outlined" onClick={() => handleClose()}>
            Odustani
          </Button>
          <Button
            id="stvoriRadionicuButton"
            variant="contained"
            onClick={() => {
              if (!radionica.id) {
                handleStvoriRadionicu(radionica);
              } else {
                handleUpdateRadionicu(radionica);
              }
            }}
          >
            {radionica.id
              ? "Ažuriraj podatke o radionici"
              : "Stvori novu radionicu"}
          </Button>
        </DialogActions>
      </Dialog>
    </>
  );
}
