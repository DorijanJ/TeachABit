import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import App from "./App.tsx";
import "./index.css";
import { GlobalContextProvider } from "./context/Global.context.tsx";
import { createTheme, CssBaseline, ThemeProvider } from "@mui/material";

const theme = createTheme({
    palette: {
        primary: {
            main: "#922728",
        },
        secondary: {
            main: "#9c27b0",
        },
        background: {
            default: "#314455",
        },
        info: {
            main: "#9c27b0",
        },
        error: {
            main: "#922728",
        },
    },
    typography: {
        fontFamily: "Roboto, sans-serif",
        h1: {
            fontSize: "2rem",
        },
    },
    spacing: 8,
});

createRoot(document.getElementById("root")!).render(
    <StrictMode>
        <ThemeProvider theme={theme}>
            <CssBaseline />
            <GlobalContextProvider>
                <App />
            </GlobalContextProvider>
        </ThemeProvider>
    </StrictMode>
);
