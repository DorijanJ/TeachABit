import { Box, IconButton, InputAdornment, TextField } from "@mui/material";
import SearchIcon from "@mui/icons-material/Search";
import { useState } from "react";
import CloseIcon from "@mui/icons-material/Close";

interface SearchBoxProps {
    onSearch: (query?: string) => void;
    height?: any;
    width?: any;
}

export default function SearchBox(props: SearchBoxProps) {
    const [query, setQuery] = useState<string>("");

    const handleSearch = () => {
        props.onSearch(query.trim());
    };

    const handleClear = () => {
        setQuery("");
        props.onSearch();
    };

    const handleKeyPress = (event: React.KeyboardEvent<HTMLInputElement>) => {
        if (event.key === "Enter") {
            handleSearch();
        }
    };

    return (
        <Box
            sx={{
                height: props.height ?? "70px",
                width: props.width ?? "30vw",

            }}
        >
            <TextField
                variant="outlined"
                placeholder="Pretraži..."
                fullWidth
                onChange={(e) => setQuery(e.target.value)}
                value={query}
                onKeyDown={handleKeyPress}
                slotProps={{
                    input: {
                        endAdornment: (
                            <InputAdornment position="start">
                                {query && (
                                    <IconButton onClick={handleClear}>
                                        <CloseIcon
                                            color="secondary"
                                            fontSize="large"
                                        />
                                    </IconButton>
                                )}
                                <IconButton onClick={handleSearch}>
                                    <SearchIcon
                                        color="secondary"
                                        fontSize="large"
                                    />
                                </IconButton>
                            </InputAdornment>
                        ),
                        spellCheck: false,
                    },
                }}
                sx={{
                    "& .MuiOutlinedInput-root": {
                        height: props.height ?? "70px",
                        borderRadius: 3,
                        backgroundColor:"#720D5D",
                    },
                    "& .MuiInputBase-input::placeholder": {
                        fontStyle: "italic",

                    },
                    "& .MuiInputBase-input": {
                        fontSize: "1.5rem",
                        color: "white",
                    },
                }}
            />
        </Box>
    );
}
