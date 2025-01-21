import { Button, Dialog, DialogActions, DialogTitle } from "@mui/material";

interface Props {
  onConfirm: () => Promise<any>;
  onClose: () => void;
  tekstPitanje: string;
  tekstOdgovor: string;
}

export default function PotvrdiPopup(props: Props) {
  return (
    <>
      <Dialog fullWidth open onClose={props.onClose}>
        <DialogTitle
        sx = {{
          height: 150,
          fontWeight: "200"
        }}
        
        >{props.tekstPitanje}</DialogTitle>
        
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
