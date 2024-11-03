import {
    Box,
    Button,
    Drawer,
    IconButton,
    List,
    ListItem,
    ListItemButton,
    ListItemIcon,
    ListItemText,
} from "@mui/material";
import BookIcon from "@mui/icons-material/Home";
import GroupIcon from "@mui/icons-material/Group";
import ForumIcon from "@mui/icons-material/Forum";
import LogoutIcon from "@mui/icons-material/Logout";
import PersonIcon from "@mui/icons-material/Person";
import { useNavigate } from "react-router-dom";
import AuthForm from "../auth/form/AuhtForm";
import { useGlobalContext } from "../../context/Global.context";
import useAuth from "../../hooks/useAuth";

export default function Navigation() {
    const navigate = useNavigate();
    const globalContext = useGlobalContext();
    const auth = useAuth();

    return (
        <>
            <Drawer
                variant="permanent"
                anchor="left"
                sx={{
                    width: 300,
                    flexShrink: 0,
                    "& .MuiDrawer-paper": {
                        width: 300,
                        boxSizing: "border-box",
                    },
                }}
            >
                <Box
                    sx={{
                        display: "flex",
                        alignItems: "center",
                        flexDirection: "column",
                        height: "100%",
                        width: "100%",
                    }}
                >
                    <List sx={{ width: "100%" }}>
                        <ListItem disablePadding>
                            <ListItemButton
                                onClick={() => navigate("/tecajevi")}
                            >
                                <ListItemIcon>
                                    <BookIcon />
                                </ListItemIcon>
                                <ListItemText primary="Tečajevi" />
                            </ListItemButton>
                        </ListItem>
                        <ListItem disablePadding>
                            <ListItemButton
                                onClick={() => navigate("/radionice")}
                            >
                                <ListItemIcon>
                                    <GroupIcon />
                                </ListItemIcon>
                                <ListItemText primary="Radionice" />
                            </ListItemButton>
                        </ListItem>
                        <ListItem disablePadding>
                            <ListItemButton onClick={() => navigate("/forumi")}>
                                <ListItemIcon>
                                    <ForumIcon />
                                </ListItemIcon>
                                <ListItemText primary="Forumi" />
                            </ListItemButton>
                        </ListItem>
                    </List>

                    <Box sx={{ flexGrow: 1 }} />

                    {globalContext.userIsLoggedIn === true &&
                        globalContext.loggedInUser && (
                            <>
                                <div
                                    style={{
                                        display: "flex",
                                        flexDirection: "column",
                                        gap: "10px",
                                        padding: "10px",
                                        alignItems: "center",
                                        width: "100%",
                                        boxSizing: "border-box",
                                    }}
                                >
                                    <Button
                                        variant="outlined"
                                        startIcon={<PersonIcon />}
                                    >
                                        {`Korisnik: ${globalContext.loggedInUser.username}`}
                                    </Button>
                                    <Button
                                        startIcon={<LogoutIcon />}
                                        variant="outlined"
                                        onClick={() => auth.logout()}
                                    >
                                        {"Odjava"}
                                    </Button>
                                </div>
                            </>
                        )}

                    {globalContext.userIsLoggedIn === false && <AuthForm />}
                </Box>
            </Drawer>
        </>
    );
}
