import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import App from "./App.tsx";
import "./index.css";
import { createTheme, CssBaseline, ThemeProvider } from "@mui/material";

const theme = createTheme({
    palette: {
        primary: {
            main: "#3a7ca5",
            light: "#F0F3FA",
            dark: "#254F6A",
        },
        secondary: {
            main: "#D9DBF1",
        },
        background: {
            default: "#F0F3FA",
        },
    },
    typography: {
        fontFamily: "Poppins, Arial, sans-serif",
        h1: {
            fontSize: "2rem",
        },
        fontWeightBold: "bolder",
    },
    spacing: 8,
});

createRoot(document.getElementById("root")!).render(
    <StrictMode>
        <ThemeProvider theme={theme}>
            <CssBaseline />
            <App />
        </ThemeProvider>
    </StrictMode>
);
