import { Box, IconButton, InputAdornment, TextField } from "@mui/material";
import SearchIcon from "@mui/icons-material/Search";
import { useState } from "react";

interface SearchBoxProps {
    onSearch: (query: string) => void;
    height?: any;
    width?: any;
}

export default function SearchBox(props: SearchBoxProps) {

    const [query, setQuery] = useState<string>("")

    const handleSearch = () => {
        if (query.trim()) {
            props.onSearch(query);
        }
    };

    const handleKeyPress = (event: React.KeyboardEvent<HTMLInputElement>) => {
        if (event.key === "Enter") {
            handleSearch();
        }
    };

    return (
        <Box
            sx={{
                height: props.height,
                width: props.width,
            }}
        >
            <TextField
                variant="outlined"
                placeholder="Pretraži..."
                fullWidth
                onChange={(e) => setQuery(e.target.value)}
                onKeyDown={handleKeyPress}
                slotProps={{
                    input: {
                        endAdornment: (
                            <InputAdornment position="start">
                                <IconButton onClick={handleSearch}>
                                    <SearchIcon color="primary" fontSize="large" />
                                </IconButton>
                            </InputAdornment>
                        ),
                    },
                }}
                sx={{
                    "& .MuiOutlinedInput-root": {
                        height: props.height,
                        paddingInline: 2,
                        borderRadius: 9,
                        backgroundColor: "#d9d9d9",
                    },
                    "& .MuiInputBase-input::placeholder": {
                        fontStyle: "italic",
                    },
                    "& .MuiInputBase-input": {
                        fontSize: "24px",
                    },
                }}
            />
        </Box>

    );
}
