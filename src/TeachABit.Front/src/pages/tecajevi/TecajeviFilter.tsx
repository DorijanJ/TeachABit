import React, { useState } from "react";
import {
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    Slider,
    TextField,
    Typography,
} from "@mui/material";

const TecajeviFilter = () => {
    const [open, setOpen] = useState(false);
    const [value1, setValue1] = useState<number>(30);
    const [value2, setValue2] = useState<number>(70);
    const [text, setText] = useState<string>("");

    const handleClickOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);

    const handleSliderChange1 = (
        _event: Event,
        newValue: number | number[]
    ) => {
        setValue1(newValue as number);
    };

    const handleSliderChange2 = (
        _event: Event,
        newValue: number | number[]
    ) => {
        setValue2(newValue as number);
    };

    const handleTextChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setText(event.target.value);
    };

    return (
        <>
            <Button
                variant="contained"
                color="primary"
                onClick={handleClickOpen}
            >
                Open Filter
            </Button>
            <Dialog open={open} onClose={handleClose}>
                <DialogContent>
                    <Typography gutterBottom>Slider 1</Typography>
                    <Slider
                        value={value1}
                        onChange={handleSliderChange1}
                        aria-labelledby="slider-1"
                        valueLabelDisplay="auto"
                        valueLabelFormat={(value) => `${value}%`}
                        min={0}
                        max={100}
                    />
                    <Typography gutterBottom>Slider 2</Typography>
                    <Slider
                        value={value2}
                        onChange={handleSliderChange2}
                        aria-labelledby="slider-2"
                        valueLabelDisplay="auto"
                        valueLabelFormat={(value) => `${value}%`}
                        min={0}
                        max={100}
                    />
                    <TextField
                        label="Text Filter"
                        variant="outlined"
                        fullWidth
                        value={text}
                        onChange={handleTextChange}
                        margin="normal"
                    />
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleClose} color="primary">
                        Cancel
                    </Button>
                    <Button onClick={handleClose} color="primary">
                        Apply
                    </Button>
                </DialogActions>
            </Dialog>
        </>
    );
};

export default TecajeviFilter;
