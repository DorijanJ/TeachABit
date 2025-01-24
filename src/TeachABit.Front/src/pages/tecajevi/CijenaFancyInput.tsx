import {
    Box,
    TextField,
    InputAdornment,
    FormControlLabel,
    Checkbox,
} from "@mui/material";
import { ChangeEvent, useState } from "react";
import { TecajDto } from "../../models/TecajDto.ts";

interface Props {
    tecaj: TecajDto;
    setTecaj: (prev: any) => void;
}

export default function CijenaFancyInput(props: Props) {
    const [previousCijena, setPreviousCijena] = useState<number | undefined>(
        props.tecaj.cijena
    );

    const handleCheckboxChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const isChecked = event.target.checked;
        if (isChecked) {
            setPreviousCijena(props.tecaj.cijena);
            props.setTecaj((prev: any) => ({
                ...prev,
                cijena: 0,
            }));
        } else {
            setPreviousCijena(props.tecaj.cijena);
            props.setTecaj((prev: any) => ({
                ...prev,
                cijena:
                    previousCijena !== undefined && previousCijena !== 0
                        ? previousCijena
                        : undefined,
            }));
        }
    };

    const handleCijenaChange = (e: ChangeEvent<HTMLInputElement>) => {
        const value = parseFloat(e.target.value);
        const decimalPlaces = value.toString().split(".")[1]?.length;
        if (!(Math.floor(value) === value) && decimalPlaces > 2) {
            return;
        }

            props.setTecaj((prev: any) => ({
                ...prev,
                cijena:
                    e.target.value !== "" ? value ?? undefined : undefined,
            }));

    };

    return (
        <Box
            sx={{
                display: "flex",
                alignItems: "center",
                gap: "20px",
            }}
        >
            <TextField
                label="Cijena"
                name="cijena"
                sx={{
                    width: "200px",
                }}
                variant="outlined"
                value={props.tecaj.cijena ?? ""}
                type="number"
                helperText="0-2000"
                slotProps={{
                    input: {
                        inputProps: {
                            min: 0,
                            max: 2000,
                            inputMode: "decimal",
                        },
                        startAdornment: (
                            <InputAdornment position="start">€</InputAdornment>
                        ),
                    },
                }}
                onChange={handleCijenaChange}
            />
            <FormControlLabel
                control={
                    <Checkbox
                        checked={props.tecaj.cijena === 0}
                        onChange={handleCheckboxChange}
                        sx={{ "& .MuiSvgIcon-root": { fontSize: 30 } }}
                    />
                }
                label="Besplatno"
                sx={{
                    marginTop: -3,
                }}
            />
        </Box>
    );
}
