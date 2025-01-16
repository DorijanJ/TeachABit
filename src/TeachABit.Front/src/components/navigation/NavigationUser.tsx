import { Button, Menu, MenuItem } from "@mui/material";
import { AppUserDto } from "../../models/AppUserDto";
import PersonIcon from "@mui/icons-material/Person";
import LogoutIcon from "@mui/icons-material/Logout";
import useAuth from "../../hooks/useAuth";
import { useNavigate } from "react-router-dom";
import { useState } from "react";

interface Props {
    user: AppUserDto;
}

export function NavigationUser(props: Props) {
    const auth = useAuth();
    const navigate = useNavigate();

    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
    const open = Boolean(anchorEl);
    const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
        setAnchorEl(event.currentTarget);
    };
    const handleClose = () => {
        setAnchorEl(null);
    };

    return (
        <>
            <div
                style={{
                    display: "flex",
                    flexDirection: "column",
                    gap: "10px",
                    padding: "10px",
                    paddingBottom: "40px",
                    alignItems: "center",
                    width: "100%",
                    boxSizing: "border-box",
                }}
            >
                <Button
                    variant="contained"
                    sx={{
                        width: "80%",
                        borderColor: "#3a7ca5",
                    }}
                    startIcon={<PersonIcon />}
                    onClick={handleClick}
                    id="navigationUser-korisnik"
                >
                    {`Korisnik: ${props.user.username}`}
                </Button>
                <Menu
                    id="basic-menu"
                    anchorEl={anchorEl}
                    open={open}
                    anchorOrigin={{
                        vertical: "top",
                        horizontal: "center",
                    }}
                    transformOrigin={{
                        vertical: "bottom",
                        horizontal: "center",
                    }}
                    onClose={handleClose}
                    MenuListProps={{
                        "aria-labelledby": "basic-button",
                    }}
                >
                    <MenuItem
                        sx={{ width: "220px" }}
                        onClick={() =>
                            navigate(`/profil/${props.user.username}`)
                        }
                    >
                        <div style={{ display: "flex", gap: "10px" }}>
                            <PersonIcon />
                            Profil
                        </div>
                    </MenuItem>
                    <MenuItem onClick={() => auth.logout()}>
                        <div style={{ display: "flex", gap: "10px" }}>
                            <LogoutIcon />
                            Odjava
                        </div>
                    </MenuItem>
                </Menu>
            </div>
        </>
    );
}
