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
                width: "32%",
                height: "400px",
                borderRadius: "10px",
                boxSizing: "border-box",
                border: "1px solid lightgray",
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
                <div>
                    {props.tecaj.opis}
                    <div>{props.tecaj.cijena}€</div>
                </div>
            </CardContent>
        </Card>
    );
}
