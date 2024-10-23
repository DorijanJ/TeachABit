import {
    Autocomplete,
    Button,
    FormControl,
    TextField,
    FormHelperText,
    Box,
    Checkbox,
    FormLabel,
    FormControlLabel,
    RadioGroup,
    Radio,
} from "@mui/material";
import { DatePicker, LocalizationProvider } from "@mui/x-date-pickers";
import { FormEvent, useState } from "react";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import dayjs, { Dayjs } from "dayjs";
import SelectTeachABit from "./components/SelectTeachABit";

interface Item {
    id: number;
    name: string;
    statusCode: number;
    special: boolean;
}

interface Account {
    id: number;
    name: string;
    desc: string;
    countryId?: number;
    blocked: boolean;
    date?: Date;
}

const itemList: Item[] = [
    { id: 1, name: "Croatia", special: true, statusCode: 22 },
    { id: 2, name: "England", special: false, statusCode: 25 },
    { id: 3, name: "France", special: false, statusCode: 27 },
    { id: 4, name: "Korea", special: false, statusCode: 28 },
    { id: 5, name: "Russia", special: false, statusCode: 21 },
];

function App() {
    const [account, setAccount] = useState<Account>({
        id: 0,
        name: "",
        desc: "",
        blocked: false,
    });
    const [selectedItem, setSelectedItem] = useState<Item | null>(null);
    const [selectedDate, setSelectedDate] = useState<Dayjs | null>(
        account.date ? dayjs(account.date) : null
    );

    const handleFormSubmit = (event: FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        console.log(account);
    };

    const handleAccountChange = (field: keyof Account, value: any) => {
        setAccount((prevAccount) => ({
            ...prevAccount,
            [field]: value ?? value,
        }));
    };

    const handleDateChange = (date: Dayjs | null) => {
        setSelectedDate(date);
        handleAccountChange("date", date?.toDate());
    };

    return (
        <LocalizationProvider dateAdapter={AdapterDayjs}>
            <Box
                component="form"
                onSubmit={handleFormSubmit}
                sx={{
                    display: "flex",
                    flexDirection: "column",
                    gap: 2,
                    width: "500px",
                    alignItems: "flex-start",
                }}
            >
                {JSON.stringify(account, null, 2)}
                {/* Name Field */}
                <FormControl fullWidth margin="normal">
                    <TextField
                        id="name"
                        value={account.name ?? ""}
                        label="Name"
                        onChange={(e) =>
                            handleAccountChange("name", e.target.value)
                        }
                        fullWidth
                    />
                    <FormHelperText>Enter your account name</FormHelperText>
                </FormControl>
                <FormControl fullWidth margin="normal">
                    <TextField
                        id="description"
                        value={account.desc ?? ""}
                        label="Description"
                        onChange={(e) =>
                            handleAccountChange("desc", e.target.value)
                        }
                        fullWidth
                    />
                    <FormHelperText>Enter your description</FormHelperText>
                </FormControl>
                <FormControlLabel
                    label="Blocked"
                    control={
                        <Checkbox
                            checked={account.blocked}
                            onChange={(e) =>
                                handleAccountChange("blocked", e.target.checked)
                            }
                        />
                    }
                />
                {/* Country Autocomplete */}
                <FormControl fullWidth margin="normal">
                    <Autocomplete
                        id="country"
                        freeSolo={false}
                        value={selectedItem}
                        options={itemList}
                        onChange={(_, value) => {
                            setSelectedItem(value);
                            handleAccountChange("countryId", value?.id);
                        }}
                        getOptionLabel={(option) => option?.name || ""}
                        renderInput={(params) => (
                            <TextField {...params} fullWidth label="Country" />
                        )}
                    />
                    <FormHelperText>
                        Select a country from the list
                    </FormHelperText>
                </FormControl>

                {/* Country Select */}
                <FormControl fullWidth margin="normal">
                    <SelectTeachABit
                        value={account.countryId ?? ""}
                        onChange={(e) => handleAccountChange("countryId", e)}
                        options={itemList}
                        label={"Country"}
                        textField={"name"}
                        itemIdField={"id"}
                        helperText="Select a country."
                        noneOptionLabel="None"
                    />
                </FormControl>

                {/* Submit Button */}
                <FormControl margin="normal">
                    <Button type="submit" variant="contained" color="primary">
                        Submit
                    </Button>
                </FormControl>

                <FormControl component="fieldset">
                    <FormLabel component="legend">Gender</FormLabel>
                    <RadioGroup
                        row
                        onChange={(e) =>
                            handleAccountChange("countryId", e.target.value)
                        }
                    >
                        <FormControlLabel
                            value={1}
                            control={<Radio />}
                            label="Croatia"
                        />
                        <FormControlLabel
                            value={2}
                            control={<Radio />}
                            label="England"
                        />
                    </RadioGroup>
                </FormControl>
                <FormControl>
                    <DatePicker
                        value={selectedDate}
                        onChange={handleDateChange}
                    />
                </FormControl>
            </Box>
        </LocalizationProvider>
    );
}

export default App;
