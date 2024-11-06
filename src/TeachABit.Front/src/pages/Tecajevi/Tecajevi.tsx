import {Card, CardContent, Typography, CardMedia, Grid2} from "@mui/material";
import { TecajDto } from "../../models/TecajDto";
import './Tecajevi.css'

const dummyData: TecajDto[] = [
    { naziv: "ASP.NET Core Tečaj" },
    { naziv: "React Tečaj" },
];

export default function Tecajevi() {
    return (
        <Grid2 container spacing={4} padding={4} >
            {dummyData.map((tecaj, index) => (
                <Grid2 key={index} >
                    <Card
                        sx={{
                            width: 300,
                            transition: "transform 0.1s ease-in-out",
                            "&:hover": {
                                transform: "scale(1.05)",
                            },
                            borderRadius: '16px'
                        }}
                    >
                        <CardMedia component="img" height="140" alt={""} />
                        <CardContent
                            sx={{
                                color: "#922728",
                                textAlign: "center",
                            }}
                        >
                            <Typography
                                variant="h4"
                                component="div"
                                sx={{ fontWeight: "bold" ,
                                    fontFamily: 'Poppins, Arial, sans-serif'}}
                            >
                                {tecaj.naziv}
                            </Typography>
                        </CardContent>
                    </Card>
                </Grid2>
            ))}
        </Grid2>
    );
}
