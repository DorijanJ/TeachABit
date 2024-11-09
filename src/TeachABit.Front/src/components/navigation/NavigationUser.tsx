import { Button } from "@mui/material";
import { AppUserDto } from "../../models/AppUserDto";
import PersonIcon from "@mui/icons-material/Person";
import LogoutIcon from "@mui/icons-material/Logout";
import useAuth from "../../hooks/useAuth";
import { useNavigate } from "react-router-dom";

interface Props {
    user: AppUserDto;
}

export function NavigationUser(props: Props) {
    const auth = useAuth();
    const navigate = useNavigate();

    return (
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
                    onClick={() => navigate(`/profil/${props.user.username}`)}
                >
                    {`Korisnik: ${props.user.username}`}
                </Button>
                <Button
                    startIcon={<LogoutIcon />}
                    variant="outlined"
                    onClick={() => auth.logout()}
                >
                    {"Odjava"}
                </Button>
            </div>
            ;
        </>
    );
}
