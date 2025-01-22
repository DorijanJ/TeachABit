import {Box, Card, CardContent, Button, Typography} from "@mui/material";
import { useRef} from "react";
import {TecajDto} from "../../models/TecajDto";


interface Props {
    tecaj: TecajDto;
}

export default function CustomSlider(props: Props) {
    const containerRef = useRef<HTMLDivElement>(null);



    const scroll = (direction: "left" | "right") => {
        if (containerRef.current) {
            const scrollAmount = direction === "left" ? -300 : 300; // Adjust scroll distance
            containerRef.current.scrollBy({left: scrollAmount, behavior: "smooth"});
        }
    };

    return (
        <Box position="relative" width="100%">
            {/* Navigation Arrows */}
            <Button
                onClick={() => scroll("left")}
                style={{
                    position: "absolute",
                    left: 0,
                    top: "50%",
                    transform: "translateY(-50%)",
                    zIndex: 1,
                }}
            >
                ←
            </Button>
            <Button
                onClick={() => scroll("right")}
                style={{
                    position: "absolute",
                    right: 0,
                    top: "50%",
                    transform: "translateY(-50%)",
                    zIndex: 1,
                }}
            >
                →
            </Button>

            <Box
                ref={containerRef}
                display="flex"
                overflow="auto"
                whiteSpace="nowrap"
                scrollBehavior="smooth"
                width="100%"
                padding="10px"
                height="20%"
            >
                {Array.from({length: 15}).map((_, index) => (
                    <Card
                        key={index}
                        style={{
                            margin: "0 10px",
                            minWidth: "200px",
                            aspectRatio: "1 / 0.75", // This ensures height equals width
                            flexShrink: 0,
                            width: "20%",
                        }}
                        sx={{
                            transition: "transform 0.2s, box-shadow 0.2s",
                            "&:hover": {
                                boxShadow: "0px 4px 20px rgba(0, 0, 0, 0.2)",
                                transform: "scale(1.03)",
                                border: "1px solid #3a7ca5",
                            },
                        }}
                    >
                        <CardContent
                            sx={{
                                textAlign: "center",
                                display: "flex",
                                flexDirection: "column",
                                height: "100%",
                                gap: "24px",
                            }}
                        >
                            <div
                                style={{
                                    display: "flex",
                                    gap: "10px",
                                    flexDirection: "column",
                                }}
                            >
                                <Box
                                    display={"flex"}
                                    flexDirection={"row"}
                                    justifyContent={"space-between"}
                                    alignItems={"flex-start"}
                                >
                                    <Typography
                                        color="primary"
                                        variant="h5"
                                        component="div"
                                        sx={{
                                            textAlign: "left",
                                            display: "-webkit-box",
                                            WebkitBoxOrient: "vertical",
                                            overflow: "hidden",
                                            WebkitLineClamp: 3,
                                            maxWidth: "100%",
                                            height: "6rem",
                                            color: "primary",
                                            marginBottom: "0px",
                                        }}
                                    >
                                        {props.tecaj.naziv}
                                    </Typography>
                                </Box>


                            </div>
                            <div
                                style={{
                                    display: "flex",
                                    width: "100%",
                                    justifyContent: "flex-end",
                                    gap: "10px",
                                    alignItems: "center",
                                }}
                            >
                                <Button
                                    variant="contained"
                                    sx={{ marginBottom: "10px" }} // Adds a bottom gap
                                >
                                    10€
                                </Button>
                            </div>
                        </CardContent>
                    </Card>
                ))}
            </Box>

        </Box>
    );
}
