import { Card, CardContent, Typography } from "@mui/material";
import { TecajDto } from "../../models/TecajDto";

interface Props {
    tecaj: TecajDto;
    onClick: () => void;
}

export default function Tecaj(props: Props) {
    return (
        <Card
            onClick={props.onClick}
            sx={{
                width: "500px",
                borderRadius: "2px",
                boxSizing: "border-box",
                border: "1px solid transparent",
                "&:hover": {
                    border: "1px solid #922728",
                },
            }}
        >
            <CardContent
                sx={{
                    textAlign: "center",
                    display: "flex",
                    flexDirection: "column",
                    gap: 1,
                }}
            >
                <Typography
                    color="primary"
                    variant="h5"
                    component="div"
                    sx={{
                        textOverflow: "ellipsis",
                        overflow: "hidden",
                        whiteSpace: "nowrap",
                        maxWidth: "100%",
                    }}
                >
                    {props.tecaj.naziv}
                </Typography>
            </CardContent>
            
        </Card>
    );
}
