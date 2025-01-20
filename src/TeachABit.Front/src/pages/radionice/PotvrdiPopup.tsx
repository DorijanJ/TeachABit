import { Box, Button, Dialog, DialogActions } from "@mui/material";

interface Props {
    onConfirm: () => Promise<any>;
    onClose: () => void;
    tekstPitanje: string;
    tekstOdgovor: string;
}

export default function PotvrdiPopup(props: Props) {
  
  return (
    <>
    <Dialog open onClose={props.onClose}>
      <Box>{props.tekstPitanje}</Box>
      <DialogActions>
        <Button variant="outlined" onClick={() => props.onClose()}>
          Odustani
        </Button>
        <Button
          variant="contained"
          onClick={() => {
            props.onConfirm();
          }}
        >
          {props.tekstOdgovor}
        </Button>
      </DialogActions>
      </Dialog>
    </>
  );
}
