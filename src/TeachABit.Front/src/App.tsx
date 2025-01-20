import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import "./App.css";
import "./styles/tiptap.css";
import useAuth from "./hooks/useAuth";
import { useEffect } from "react";
import GenericRoute from "./components/auth/routing/GenericRoute";
import { observer } from "mobx-react";
import globalStore from "./stores/GlobalStore";
import { Backdrop, CircularProgress } from "@mui/material";
import PublicRoute from "./components/auth/routing/PublicRoute";
import Forum from "./pages/forum/Forum";
import Profil from "./pages/profil/Profil";
import "@fortawesome/fontawesome-free/css/all.min.css";
import Tecajevi from "./pages/tecajevi/Tecajevi";
import Radionice from "./pages/radionice/Radionice";
import ConfirmEmail from "./pages/confirmEmail_temp/ConfirmEmail";
import ResetPassword from "./pages/resetPassword/ResetPassword";
import ObjavaPage from "./pages/forum/ObjavaPage";
import Notification from "./components/notification/Notification";
import RadionicaPage from "./pages/radionice/RadionicaPage";
import TecajPage from "./pages/tecajevi/TecajPage";
import PrivateRoute from "./components/auth/routing/PrivateRoute";
import KorisniciAdministracijaPage from "./pages/administracija/KorisniciAdministracijaPage";

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

            {globalStore.globalNotifications.map((n) => (
                <Notification
                    key={n.id}
                    message={n.message}
                    severity={n.severity}
                    onClose={() => globalStore.clearNotification(n.id)}
                />
            ))}

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
                            path="forum"
                            element={
                                <GenericRoute page={<Forum />} withNavigation />
                            }
                        />
                        <Route
                            path="confirm-email"
                            element={<PublicRoute page={<ConfirmEmail />} />}
                        />
                        <Route
                            path="reset-password"
                            element={<PublicRoute page={<ResetPassword />} />}
                        />
                        <Route
                            path="profil/:username"
                            element={
                                <GenericRoute
                                    page={<Profil />}
                                    withNavigation
                                />
                            }
                        />
                        <Route
                            path="objava/:objavaId"
                            element={
                                <GenericRoute
                                    page={<ObjavaPage />}
                                    withNavigation
                                />
                            }
                        />

                        <Route
                            path="radionica/:radionicaId"
                            element={
                                <GenericRoute
                                    page={<RadionicaPage />}
                                    withNavigation
                                />
                            }
                        />
                        <Route
                            path="tecajevi/:tecajId"
                            element={
                                <GenericRoute
                                    page={<TecajPage />}
                                    withNavigation
                                />
                            }
                        />
                        <Route
                            path="korisnici-administracija"
                            element={
                                <PrivateRoute
                                    page={<KorisniciAdministracijaPage />}
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
