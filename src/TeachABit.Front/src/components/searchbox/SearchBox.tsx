import { Box, InputAdornment, TextField } from "@mui/material";
import SearchIcon from '@mui/icons-material/Search';

export default function SearchBox() {


    return(
        <Box
            sx={{
                width: "600px",
                height: "80px",
                marginTop: "20px",
            }}
        >
            <TextField 
                variant="outlined"
                placeholder="Pretraži..."
                slotProps={{
                    input: {
                        endAdornment: (
                            <InputAdornment position="start">
                                <SearchIcon htmlColor="#922728" fontSize="large"/>
                            </InputAdornment>
                        ),
                    },
                }}
                sx={{
                    width: "100%",
                    '& .MuiOutlinedInput-root': {
                        paddingInline: "25px",
                        borderRadius: '50px',
                        height: "75px",
                        backgroundColor: "#d9d9d9",
                    },
                    '& .MuiInputBase-input::placeholder': {
                        fontStyle: 'italic',
                    },
                    '& .MuiInputBase-input': {
                        fontSize: "28px",
                    },
                }}
            >
                
            </TextField>
        </Box>
    );
}