import {useEffect, useRef, useState} from "react";
import { TecajDto } from "../../models/TecajDto";
import requests from "../../api/agent";
import {Box, Button, Card, CardContent, Typography} from "@mui/material";


import useRequestBuilder from "../../hooks/useRequestBuilder";
import {useNavigate} from "react-router-dom";

export default function CustomSliderTecaj(){

    const [tecajList, setTecajList] = useState<TecajDto[]>([]);


    const { buildRequest } = useRequestBuilder();




    const GetTecajList = async (search: string | undefined = undefined) => {
        const response = await requests.getWithLoading(
            buildRequest("tecajevi", { search })
        );
        if (response && response.data) setTecajList(response.data);
    };

    useEffect(() => {
        GetTecajList();
    }, []);

    const containerRef = useRef<HTMLDivElement>(null);



    const scroll = (direction: "left" | "right") => {
        if (containerRef.current) {
            const scrollAmount = direction === "left" ? -300 : 300; // Adjust scroll distance
            containerRef.current.scrollBy({left: scrollAmount, behavior: "smooth"});
        }
    };

    const navigate = useNavigate();

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
                {tecajList.map((tecaj) => (
                    <Card
                        key={tecaj.id}
                        style={{
                            margin: "0 10px",
                            minWidth: "200px",
                            aspectRatio: "1 / 0.75", // Ensures width-to-height ratio
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
                            borderRadius: "10px",
                            boxSizing: "border-box",
                            border: "1px solid lightgray",
                            cursor: "pointer",
                        }}
                        onClick={() => {
                                navigate(`/tecajevi/${tecaj.id}`);
                        }}
                    >
                        <CardContent
                            sx={{
                                display: "flex",
                                flexDirection: "column",
                                justifyContent: "space-between", // Spreads content with space between sections
                                height: "100%",
                                padding: "16px", // Adds padding around the content
                            }}
                        >
                            {/* Top Section */}
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
                                        }}
                                    >
                                        {tecaj.naziv}
                                    </Typography>
                                </Box>
                            </div>

                            {/* Button Section */}
                            <div
                                style={{
                                    display: "flex",
                                    justifyContent: "flex-end",
                                    alignItems: "center",
                                    marginTop: "auto", // Pushes this section to the bottom
                                    marginBottom: "8px", // Adds space below the button
                                }}
                            >
                                <Button
                                    variant="contained"

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
