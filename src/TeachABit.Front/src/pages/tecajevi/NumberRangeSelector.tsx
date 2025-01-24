import { Slider } from "@mui/material";
import React from "react";

interface NumberRangeSelectorProps {
    min: number;
    max: number;
    minDistance?: number | undefined;
    onChangeCommited: (value: number[]) => void;
    onChange: (value: number[]) => void;
}

const NumberRangeSelector: React.FC<NumberRangeSelectorProps> = ({
    min,
    max,
    minDistance,
    onChange,
    onChangeCommited,
}) => {
    const [value, setValue] = React.useState<number[]>([min, max]);

    const updateValues = (v: number[]) => {
        setValue(v);
        onChange(v);
    };

    const handleChangeCommitted = (
        _event: React.SyntheticEvent | Event,
        value: number | number[]
    ) => {
        if (Array.isArray(value)) {
            onChangeCommited(value);
        }
    };

    const handleChange = (
        _event: Event,
        newValue: number | number[],
        activeThumb: number
    ) => {
        if (!Array.isArray(newValue)) {
            return;
        }

        if (activeThumb === 0) {
            updateValues([
                Math.min(newValue[0], value[1] - (minDistance ?? 0)),
                value[1],
            ]);
        } else {
            updateValues([
                value[0],
                Math.max(newValue[1], value[0] + (minDistance ?? 0)),
            ]);
        }
    };

    return (
        <div>
            <Slider
                value={value}
                onChange={handleChange}
                onChangeCommitted={handleChangeCommitted}
                valueLabelDisplay="auto"
                disableSwap
                min={min}
                max={max}
            />
        </div>
    );
};

export default NumberRangeSelector;
