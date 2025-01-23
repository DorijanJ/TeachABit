import { Box, Button, Dialog, DialogActions, DialogContent, DialogTitle } from "@mui/material";
/*import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { DateTimePicker } from "@mui/x-date-pickers/DateTimePicker";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider/LocalizationProvider";
import { renderTimeViewClock } from "@mui/x-date-pickers/timeViewRenderers";
import { ChangeEvent } from "react";*/
import { RadionicaDto } from "../../models/RadionicaDto";
//import Radionica from "./Radionica";
import UserLink from "../profil/UserLink";
import TeachABitRenderer from "../../components/editor/TeachaBitRenderer";
import Radionica from "./Radionica";

interface Props {
    onConfirm: () => Promise<any>;
    onClose: () => void;
    radionica?: RadionicaDto;
}

export default function RadionicaPopup(props: Props) {

  return (
    <>
    <Dialog
        open
        maxWidth={"md"}
    >
        <DialogTitle >
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
                    {props.radionica?.naziv}
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
                width: "100%",
            }}

            /*
          bilo bi dobro tu imat nekakvu sliku al je nema trenutno
        */
        >
            {/* 
            <TextField
                fullWidth
                autoFocus
                label="Naziv radionice"
                name="naziv"
                variant="outlined"
                //required = {true}
                
            />
              
            {/* <Box
                sx={{
                    border: "blue 1px dotted",
                }}
            >
                Slika neka
            </Box> }
            */}

                        <Box
                            flexDirection={"row"}
                            alignItems={"center"}
                            display={"flex"}
                            justifyContent={"space-between"}
                            gap="5px"
                        >
                            <Box></Box>
                            <UserLink
                                user={{
                                    id: props.radionica?.vlasnikId,
                                    username: props.radionica?.vlasnikUsername,
                                    profilnaSlikaVersion:
                                        props.radionica?.vlasnikProfilnaSlikaVersion,
                                }}
                            />
                        </Box>
{/*
            <TextField
                fullWidth
                autoFocus
                label="Opis radionice"
                name="opis"
                variant="outlined"
                multiline
                rows={2}
                //required = {true}
                //value={radionica.opis || ""}
                /*onChange={(e: ChangeEvent<HTMLInputElement>) =>
                    setRadionica((prev: any) => ({
                        ...prev,
                        opis: e.target.value,
                    }))
                }
            />*/}

            <TeachABitRenderer content={props.radionica?.opis ?? ""} />

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
                <Box
                    //autoFocus
                    //label="Kapacitet"
                    //variant="outlined"
                    sx={{
                        width: "30%",
                    }}
                    
                >
                    {props.radionica?.maksimalniKapacitet}

                </Box>
 
                <Box sx={{
                        width: "30%",
                    }}
                    >
                    {props.radionica?.cijena}
                    
                    </Box>

                <Box>
                    {/*props.radionica?.vrijemeRadionice || undefined*/}
                </Box>

                {/*<LocalizationProvider dateAdapter={AdapterDayjs}>
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
                </LocalizationProvider>*/}
            </div>
        </DialogContent>

        <DialogActions>
            <Button variant="outlined" onClick={props.onClose}>
                Odustani
            </Button>
            <Button
                //disabled = {!radionica.cijena}
                variant="contained"

                /*tu treba dodat plaćanje!!!*/

                /*onClick={() => {
                    if (!radionica.id) {
                        handleStvoriRadionicu(radionica);
                    } else {
                        handleUpdateRadionicu(radionica);
                    }
                }}*/
            >
                {props.radionica?.cijena}€
            </Button>
        </DialogActions>
    </Dialog>
</>
);

}
