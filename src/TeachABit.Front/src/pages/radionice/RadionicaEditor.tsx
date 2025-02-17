import {
    Dialog,
    DialogTitle,
    DialogContent,
    TextField,
    DialogActions,
    Button,
    InputAdornment,
} from "@mui/material";
import { useState, ChangeEvent } from "react";
import requests from "../../api/agent";
import { RadionicaDto } from "../../models/RadionicaDto";
import { DateTimePicker } from "@mui/x-date-pickers/DateTimePicker";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider/LocalizationProvider";
import { renderTimeViewClock } from "@mui/x-date-pickers/timeViewRenderers";
import dayjs from "dayjs";
import ImageUploadComponent from "../../components/ImageUploadComponent";
import { CreateOrUpdateRadionicaDto } from "../../models/CreateOrUpdateRadionicaDto";
interface Props {
    refreshData: () => Promise<any>;
    onClose: () => void;
    isOpen: boolean;
    radionica?: RadionicaDto;
}

export default function RadionicaEditor(props: Props) {
    const [base64image, setBase64Image] = useState<string>();

    const [radionica, setRadionica] = useState<RadionicaDto>({
        naziv: props.radionica?.naziv ?? "",
        id: props.radionica?.id,
        opis: props.radionica?.opis ?? "",
        vlasnikId: props.radionica?.vlasnikId,
        vlasnikUsername: props.radionica?.vlasnikUsername,
        vlasnikProfilnaSlikaVersion:
            props.radionica?.vlasnikProfilnaSlikaVersion,
        brojprijavljenih: props.radionica?.brojprijavljenih ?? 0,
        maksimalniKapacitet: props.radionica?.maksimalniKapacitet,
        vrijemeRadionice: props.radionica?.vrijemeRadionice,
        cijena: props.radionica?.cijena,
        naslovnaSlikaVersion: props.radionica?.naslovnaSlikaVersion,
    });

    const handleClose = (reload: boolean = false) => {
        setRadionica({
            naziv: "",
            opis: "",
            cijena: undefined,
            vrijemeRadionice: undefined,
            maksimalniKapacitet: undefined,
            naslovnaSlikaVersion: undefined,
        });
        props.onClose();
        if (reload) props.refreshData();
    };

  const handleUpdateRadionicu = async (radionica: RadionicaDto) => {
    if (!radionica.cijena || !radionica.vrijemeRadionice) return;
    const updateRadionicaDto: CreateOrUpdateRadionicaDto = {
      id: radionica.id,
      naziv: radionica.naziv,
      opis: radionica?.opis ?? "",
      cijena: radionica.cijena,
      maksimalniKapacitet: radionica?.maksimalniKapacitet,
      vrijemeRadionice: radionica?.vrijemeRadionice,
      naslovnaSlikaBase64: base64image,
    };
    const response = await requests.putWithLoading(
      "radionice",
      updateRadionicaDto
    );
    if (response && response.data) {
      handleClose(true);
    }
  };

    const handleStvoriRadionicu = async (
        radionica: RadionicaDto //nisam siguran jel tu treba ić samo CreateOrUpdateRadionicaDto?
    ) => {
        if (!radionica.cijena || !radionica.vrijemeRadionice) return;
        const createRadionicaDto: CreateOrUpdateRadionicaDto = {
            id: radionica.id,
            naziv: radionica.naziv,
            opis: radionica?.opis ?? "",
            cijena: radionica.cijena,
            maksimalniKapacitet: radionica?.maksimalniKapacitet,
            vrijemeRadionice: radionica?.vrijemeRadionice,
            naslovnaSlikaBase64: base64image,
        };

        const response = await requests.postWithLoading(
            "radionice",
            createRadionicaDto
        );
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

                    {/* <Box
                        sx={{
                            border: "blue 1px dotted",
                        }}
                    >
                        Slika neka
                    </Box> */}

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
                        title="kapacitet-cijena-vrijeme-wrapper"
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
                            value={radionica.maksimalniKapacitet || ""}
                            onChange={(e: ChangeEvent<HTMLInputElement>) => {
                                const numericValue = e.target.value.replace(
                                    /[^0-9]/g,
                                    ""
                                );
                                setRadionica((prev: any) => ({
                                    ...prev,
                                    maksimalniKapacitet: numericValue,
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
              //type="number"
              slotProps={{
                input: {
                  startAdornment: (
                    <InputAdornment position="start">€</InputAdornment>
                  ),
                },
              }}
              value={radionica.cijena || ""}
              onChange={(e: ChangeEvent<HTMLInputElement>) => {
                const value = e.target.value.replace(/[^0-9]/g, ""); //POPRAVI DA SE MOGU DECIMALNI UNOSIT
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
                                value={
                                    radionica.vrijemeRadionice
                                        ? dayjs(radionica.vrijemeRadionice)
                                        : null
                                }
                                disablePast={true}
                                viewRenderers={{
                                    hours: renderTimeViewClock,
                                    minutes: renderTimeViewClock,
                                    seconds: renderTimeViewClock,
                                }}
                                onChange={(e) =>
                                    setRadionica((prev: any) => ({
                                        ...prev,
                                        vrijemeRadionice: e,
                                    }))
                                }
                            />
                        </LocalizationProvider>
                    </div>

          <div
            style={{
              display: "flex",
              flexDirection: "column",
              gap: "10px",
            }}
          >
            <ImageUploadComponent
              setFile={(file: string) => {
                setBase64Image(file);
              }}
              ratio="2/1"
              width={"70%"}
            />
          </div>
        </DialogContent>

                <DialogActions>
                    <Button variant="outlined" onClick={() => handleClose()}>
                        Odustani
                    </Button>
                    <Button
                        disabled={!radionica.cijena || !radionica.naziv}
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
