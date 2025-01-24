import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  TextField,
} from "@mui/material";
import { RadionicaDto } from "../../models/RadionicaDto";

import { ObavijestDto } from "../../models/ObavijestDto";
import { ChangeEvent, useState } from "react";

interface Props {
  onConfirm: (obavijest: ObavijestDto) => Promise<any>;
  onClose: () => void;
  radionica: RadionicaDto;
  obavijest: ObavijestDto;
}

export default function RadionicaPopup(props: Props) {
  const [obavijest, setObavijest] = useState<ObavijestDto>(
    props.obavijest || {
      naslov: "",
      poruka: "",
    }
  );

  const handleConfirm = async () => {
    await props.onConfirm(obavijest);
    props.onClose();
  };
  return (
    <>
      <Dialog open onClose={props.onClose} maxWidth="md" fullWidth>
        <DialogTitle>
          <TextField
            fullWidth
            autoFocus
            label="Naslov obavijesti"
            variant="outlined"
            value={obavijest.naslov}
          />
        </DialogTitle>

        <DialogContent>
          <TextField
            fullWidth
            label="Opis obavijesti"
            variant="outlined"
            multiline
            rows={4}
            value={obavijest.poruka}
          />
        </DialogContent>

        <DialogActions>
          <Button variant="outlined" onClick={props.onClose}>
            Odustani
          </Button>
          <Button variant="contained" onClick={handleConfirm}>
            Pošalji
          </Button>
        </DialogActions>
      </Dialog>
    </>
  );
}
