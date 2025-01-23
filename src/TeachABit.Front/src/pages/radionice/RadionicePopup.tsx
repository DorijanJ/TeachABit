import { Button, Dialog, DialogActions, DialogContent, DialogTitle, InputAdornment, TextField } from "@mui/material";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { DateTimePicker } from "@mui/x-date-pickers/DateTimePicker";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider/LocalizationProvider";
import { renderTimeViewClock } from "@mui/x-date-pickers/timeViewRenderers";
import { RadionicaDto } from "../../models/RadionicaDto";

interface Props {
    refreshData: () => Promise<any>;
    onClose: () => void;
    radionica?: RadionicaDto;
}

export default function RadionicaPopup(props: Props) {


    const handleClose = (reload: boolean = false) => {
        props.onClose();
        if (reload) props.refreshData();
    };

    return (
        <>
            <Dialog
                open
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
                    //value={radionica.opis || ""}
                    /*onChange={(e: ChangeEvent<HTMLInputElement>) =>
                        setRadionica((prev: any) => ({
                            ...prev,
                            opis: e.target.value,
                        }))
                    }*/
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
                        //value={radionica.kapacitet || ""}
                        // onChange={(e: ChangeEvent<HTMLInputElement>) => {
                        //     const numericValue = e.target.value.replace(
                        //         /[^0-9]/g,
                        //         ""
                        //     );
                        //     /*setRadionica((prev: any) => ({
                        //         ...prev,
                        //         kapacitet: numericValue,
                        //     }));*/
                        // }}
                        />

                        <TextField
                            autoFocus
                            label="Cijena"
                            name="cijena"
                            sx={{
                                width: "30%",
                            }}
                            variant="outlined"
                            //value={radionica.cijena?.toString() || null}
                            //type="number"
                            slotProps={{
                                input: {
                                    startAdornment: (
                                        <InputAdornment position="start">
                                            €
                                        </InputAdornment>
                                    ),
                                },
                            }}
                        //             onChange={(e: ChangeEvent<HTMLInputElement>) => {
                        //                 const value = e.target.value.replace(
                        //                     /[^0-9,.]/g,
                        //                     ""
                        //                 );
                        //                 /*const decimalPlaces = value.toString().split(".")[1]?.length;
                        // if (!(Math.floor(value) === value) && decimalPlaces > 2) {
                        //   return;
                        // }*/
                        //                 /*setRadionica((prev: any) => ({
                        //                     ...prev,
                        //                     cijena: value,
                        //                 }));*/
                        //             }}
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
                        //disabled = {!radionica.cijena}
                        id="stvoriRadionicuButton"
                        variant="contained"
                    /*onClick={() => {
                        if (!radionica.id) {
                            handleStvoriRadionicu(radionica);
                        } else {
                            handleUpdateRadionicu(radionica);
                        }
                    }}*/
                    >
                        {/*radionica.id
                    ? "Ažuriraj podatke o radionici"
                    : "Stvori novu radionicu"*/}
                    </Button>
                </DialogActions>
            </Dialog>
        </>
    );

}
