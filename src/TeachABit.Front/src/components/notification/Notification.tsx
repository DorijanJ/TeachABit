import { Alert, Snackbar } from "@mui/material";

interface Props {
    message: string;
    severity: "info" | "warning" | "error" | "success";
    onClose: () => void;
}
export default function Notification(props: Props) {
    return (
        <>
            <Snackbar
                anchorOrigin={{
                    vertical: "top",
                    horizontal: "right",
                }}
                open
                autoHideDuration={1000}
                onClose={props.onClose}
            >
                <Alert
                    onClose={props.onClose}
                    severity={props.severity}
                    variant="filled"
                    sx={{ width: "100%" }}
                >
                    {props.message}
                </Alert>
            </Snackbar>
        </>
    );
}
