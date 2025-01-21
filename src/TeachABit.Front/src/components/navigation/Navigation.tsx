import { Box, Drawer, IconButton, List } from "@mui/material";
import ForumIcon from "@mui/icons-material/Forum";
import AuthForm from "../auth/form/AuthForm";
import { useGlobalContext } from "../../context/Global.context";
import localStyles from "./Navigation.module.css";
import Logo from "../../images/logo.png";
import NavigationItem from "./NavigationItem";
import { NavigationUser } from "./NavigationUser";
import PeopleIcon from "@mui/icons-material/People";
import NavigateBeforeIcon from "@mui/icons-material/NavigateBefore";
import { useLocation } from "react-router-dom";
import { useState } from "react";
import { LaptopChromebook, MenuBook } from "@mui/icons-material";

interface Props {
    isExpanded?: boolean | undefined;
}

export default function Navigation(props: Props) {
    const globalContext = useGlobalContext();
    const location = useLocation();
    const isActive = (route: string) => location.pathname === route;

    const [isExpanded, setIsExpanded] = useState(props.isExpanded ?? false);

    return (
        <>
            <Drawer
                variant="persistent"
                anchor="left"
                open
                sx={{
                    flexShrink: 0,
                    width: isExpanded ? 300 : 75,
                    "& .MuiDrawer-paper": {
                        width: isExpanded ? 300 : 75,
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
                            sx={{
                                transform: isExpanded
                                    ? "rotate(0deg)"
                                    : "rotate(180deg)",
                                //justifyContent: isExpanded? "" : "center",
                                //padding: isExpanded ? "20px" : "100px",
                                color: "#3a7ca5",
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
                            route={"/forum"}
                            name={"Forum"}
                            isActive={isActive("/forum")}
                            icon={
                                <ForumIcon
                                    color="primary"
                                    className={localStyles.navImage}
                                />
                            }
                            isExpanded={isExpanded}
                        />
                        {globalContext.isAdmin && (
                            <NavigationItem
                                route={"/korisnici-administracija"}
                                name={"Korisnici"}
                                isActive={isActive("/korisnici-administracija")}
                                icon={
                                    <PeopleIcon
                                        color="primary"
                                        className={localStyles.navImage}
                                    />
                                }
                                isExpanded={isExpanded}
                            />
                        )}
                    </List>

          <Box sx={{ flexGrow: 1 }} />

          {globalContext.userIsLoggedIn === true &&
            globalContext.currentUser &&
            isExpanded && <NavigationUser user={globalContext.currentUser} />}

          {globalContext.userIsLoggedIn === false && isExpanded && <AuthForm />}
        </Box>
      </Drawer>
    </>
  );
}
