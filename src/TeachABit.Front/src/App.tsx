import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import "./App.css";
import useAuth from "./hooks/useAuth";
import { useEffect } from "react";
import GenericRoute from "./components/auth/routing/GenericRoute";
import Tecajevi from "./pages/Tecajevi/Tecajevi";
import { GlobalContextProvider } from "./context/Global.context";

function App() {
    const auth = useAuth();

    useEffect(() => {
        auth.handleUserLoggedInCheck();
    }, []);

    return (
        <>
            <BrowserRouter>
                <div className="appContainer">
                    <Routes>
                        <Route
                            path="tecajevi"
                            element={
                                <GenericRoute
                                    page={<Tecajevi />}
                                    withNavigation
                                />
                            }
                        />
                        <Route
                            path="radionice"
                            element={
                                <GenericRoute page={<></>} withNavigation />
                            }
                        />
                        <Route
                            path="forumi"
                            element={
                                <GenericRoute page={<></>} withNavigation />
                            }
                        />
                        <Route
                            path="*"
                            element={<Navigate to={"/tecajevi"} />}
                        />
                    </Routes>
                </div>
            </BrowserRouter>
        </>
    );
}

export default App;
