import {
    ListItem,
    ListItemButton,
    ListItemIcon,
    ListItemText,
} from "@mui/material";
import { useNavigate } from "react-router-dom";
import localStyles from "./Navigation.module.css";

interface Props {
    route: string;
    name: string;
    icon: JSX.Element;
    isActive: boolean;
    isExpanded: boolean;
}

export default function NavigationItem(props: Props) {
    const navigate = useNavigate();

    return (
        <ListItem disablePadding>
            <ListItemButton
                onClick={() => navigate(props.route)}
                className={
                    localStyles.navButton +
                    (props.isActive ? ` ${localStyles.activeItem}` : "")
                }
            >
                <ListItemIcon
                    sx={{
                        width: "30px",
                        minWidth: "unset",
                        padding: props.isExpanded ? "20px" : "0px",
                    }}
                >
                    {props.icon}
                </ListItemIcon>
                {props.isExpanded && (
                    <ListItemText
                        primary={props.name}
                        classes={{
                            primary: localStyles.listItemText,
                        }}
                    />
                )}
            </ListItemButton>
        </ListItem>
    );
}
