import Box from "@mui/material/Box";
import Navigation from "../../navigation/Navigation";
import SearchBox from "../../searchbox/SearchBox";

interface GenericRouteProps {
    page: JSX.Element;
    withNavigation?: boolean;
    withSearchBox?: boolean;
}

export default function GenericRoute(props: GenericRouteProps) {
    return (
        <>
            {props.withNavigation && <Navigation />}
            <Box
                sx={{
                    display: "flex",
                    justifyContent: "center",
                    width: "100%",
                }}
            >
                <Box
                    sx={{
                        display: "flex", 
                        flexDirection: "column",
                        alignItems: "center",
                    }}
                >
                    {props.withSearchBox && <SearchBox />}
                    {props.page}
                </Box>
                
            </Box>
        </>
    );
}
