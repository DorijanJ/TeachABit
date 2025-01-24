import { Button } from "@mui/material";
import PotvrdiPopup from "../../components/dialogs/PotvrdiPopup";
import { useState } from "react";

interface Props {
    deleteRacun: () => Promise<any>;
}

export default function DeleteButtonAndPrompt(props: Props) {
    const [isPotvrdaOpen, setIsPotvrdaOpen] = useState(false);

    return (
        <>
            <Button
                sx={{ height: "30px" }}
                color="error"
                variant="contained"
                onClick={() => setIsPotvrdaOpen(true)}
            >
                Izbriši račun
            </Button>
            {isPotvrdaOpen && (
                <PotvrdiPopup
                    tekstPitanje="Jeste li sigurni da želite izbrisati ovaj račun?"
                    tekstOdgovor="Izbriši"
                    onConfirm={() => props.deleteRacun()}
                    onClose={() => setIsPotvrdaOpen(false)}
                />
            )}
        </>
    );
}
