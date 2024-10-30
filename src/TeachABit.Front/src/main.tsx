import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import App from "./App.tsx";
import "./index.css";
import { GlobalContextProvider } from "./context/Global.context.tsx";

createRoot(document.getElementById("root")!).render(
    <StrictMode>
        <GlobalContextProvider>
            <App />
        </GlobalContextProvider>
    </StrictMode>
);
