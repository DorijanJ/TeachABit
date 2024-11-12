import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import "./App.css";
import "./styles/tiptap.css";
import useAuth from "./hooks/useAuth";
import { useEffect } from "react";
import GenericRoute from "./components/auth/routing/GenericRoute";
import Tecajevi from "./pages/tecajevi_temp/Tecajevi";
import { observer } from "mobx-react";
import globalStore from "./stores/GlobalStore";
import { Backdrop, CircularProgress } from "@mui/material";
import PublicRoute from "./components/auth/routing/PublicRoute";
import ConfirmEmail from "./pages/confirmEmail/ConfirmEmail";
import ResetPassword from "./pages/resetPassword_temp/ResetPassword";
import Forum from "./pages/forum/Forum";
import PrivateRoute from "./components/auth/routing/PrivateRoute";
import Profil from "./pages/profil/Profil";
import "@fortawesome/fontawesome-free/css/all.min.css";
import Radionice from "./pages/radionice_temp/Radionice";

const App = observer(() => {
    const auth = useAuth();

    useEffect(() => {
        auth.handleUserLoggedInCheck();
    }, []);

    return (
        <>
            {globalStore.isPageLoading && (
                <Backdrop
                    sx={{
                        color: "#fff",
                        zIndex: 1600,
                    }}
                    open
                >
                    <CircularProgress color="inherit" />
                </Backdrop>
            )}
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
                                <GenericRoute
                                    page={<Radionice />}
                                    withNavigation
                                />
                            }
                        />
                        <Route
                            path="forumi"
                            element={
                                <GenericRoute page={<Forum />} withNavigation />
                            }
                        />
                        <Route
                            path="confirm-email"
                            element={
                                <PublicRoute
                                    page={<ConfirmEmail />}
                                    withNavigation
                                />
                            }
                        />
                        <Route
                            path="reset-password"
                            element={
                                <PublicRoute
                                    page={<ResetPassword />}
                                    withNavigation
                                />
                            }
                        />
                        <Route
                            path="profil/:username"
                            element={
                                <PrivateRoute
                                    page={<Profil />}
                                    withNavigation
                                />
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
});

export default App;
