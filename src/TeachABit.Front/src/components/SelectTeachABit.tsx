import {
    Select,
    MenuItem,
    FormControl,
    FormHelperText,
    SelectChangeEvent,
    InputLabel,
} from "@mui/material";

interface SelectTeachABitProps<T> {
    value: string | number | undefined;
    onChange: (value: number | string | undefined) => void;
    options: T[];
    textField: keyof T;
    itemIdField: keyof T;
    label: string;
    helperText?: string;
    noneOptionLabel?: string;
}

function SelectTeachABit<T>(props: SelectTeachABitProps<T>) {
    const handleChange = (event: SelectChangeEvent<string | number>) => {
        const selectedValue =
            event.target.value === "" ? undefined : event.target.value;
        props.onChange(selectedValue);
    };

    return (
        <FormControl fullWidth margin="normal">
            <InputLabel id="select-teach-a-bit-label">{props.label}</InputLabel>
            <Select
                labelId="select-teach-a-bit-label"
                value={props.value} // Directly pass the value
                onChange={handleChange}
                displayEmpty
            >
                <MenuItem value={undefined}>
                    <em>None</em>
                </MenuItem>
                {props.options.map((option) => (
                    <MenuItem
                        key={option[props.itemIdField] as string | number}
                        value={option[props.itemIdField] as string | number}
                    >
                        {option[props.textField] as string}
                    </MenuItem>
                ))}
            </Select>
            {props.helperText && (
                <FormHelperText>{props.helperText}</FormHelperText>
            )}
        </FormControl>
    );
}

export default SelectTeachABit;
