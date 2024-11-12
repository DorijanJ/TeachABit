import { Box, Drawer, IconButton, List } from "@mui/material";
import BookIcon from "@mui/icons-material/Home";
import GroupIcon from "@mui/icons-material/Group";
import ForumIcon from "@mui/icons-material/Forum";
import AuthForm from "../auth/form/AuthForm";
import { useGlobalContext } from "../../context/Global.context";
import localStyles from "./Navigation.module.css";
import Logo from "../../images/logo.png";
import NavigationItem from "./NavigationItem";
import { NavigationUser } from "./NavigationUser";
import NavigateBeforeIcon from "@mui/icons-material/NavigateBefore";
import { useLocation } from "react-router-dom";
import { useState } from "react";

export default function Navigation() {
    const globalContext = useGlobalContext();
    const location = useLocation();
    const isActive = (route: string) => location.pathname === route;
    const [isExpanded, setIsExpanded] = useState(true);

    return (
        <>
            <Drawer
                variant="persistent"
                anchor="left"
                open
                sx={{
                    flexShrink: 0,
                    width: isExpanded ? 300 : 65,
                    "& .MuiDrawer-paper": {
                        width: isExpanded ? 300 : 65,
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
                        backgroundColor: "#D9D9D9",
                    }}
                >
                    <IconButton
                        sx={{
                            width: "60px",
                            height: "60px",
                            position: "absolute",
                            alignSelf: "flex-end",
                        }}
                        onClick={() => setIsExpanded((prev) => !prev)}
                    >
                        <NavigateBeforeIcon
                            color="primary"
                            sx={{
                                transform: isExpanded
                                    ? "rotate(0deg)"
                                    : "rotate(180deg)",
                                transition: "transform 0.3s ease", // dodaje animiranu tranziciju
                            }}
                        />
                    </IconButton>
                    {isExpanded ? (
                        <img
                            src={Logo}
                            alt="Teach A Bit Logo"
                            style={{
                                width: "150px",
                                marginTop: "30px",
                            }}
                        />
                    ) : (
                        <Box
                            style={{ height: "150px", marginTop: "30px" }}
                        ></Box>
                    )}
                    <List
                        sx={{
                            width: "100%",
                            display: "flex",
                            gap: "10px",
                            flexDirection: "column",
                            marginTop: "60px",
                            padding: isExpanded ? "0 20px" : "0",
                        }}
                    >
                        <NavigationItem
                            route={"/tecajevi"}
                            name={"Tečajevi"}
                            isActive={isActive("/tecajevi")}
                            icon={
                                <BookIcon
                                    color="primary"
                                    className={localStyles.navImage}
                                />
                            }
                            isExpanded={isExpanded}
                        />
                        <NavigationItem
                            route={"/radionice"}
                            name={"Radionice"}
                            isActive={isActive("/radionice")}
                            icon={
                                <GroupIcon
                                    color="primary"
                                    className={localStyles.navImage}
                                />
                            }
                            isExpanded={isExpanded}
                        />
                        <NavigationItem
                            route={"/forumi"}
                            name={"Forumi"}
                            isActive={isActive("/forumi")}
                            icon={
                                <ForumIcon
                                    color="primary"
                                    className={localStyles.navImage}
                                />
                            }
                            isExpanded={isExpanded}
                        />
                    </List>

                    <Box sx={{ flexGrow: 1 }} />

                    {globalContext.userIsLoggedIn === true &&
                        globalContext.loggedInUser &&
                        isExpanded && (
                            <NavigationUser user={globalContext.loggedInUser} />
                        )}

                    {globalContext.userIsLoggedIn === false && isExpanded && (
                        <AuthForm />
                    )}
                </Box>
            </Drawer>
        </>
    );
}
