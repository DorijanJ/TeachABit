import { useRef } from "react";
import { Box, Button } from "@mui/material";
import { RadionicaDto } from "../../models/RadionicaDto";
import Radionica from "../radionice/Radionica";

interface Props {
    radionice: RadionicaDto[];
}

export default function CustomSliderRadionica({ radionice }: Props) {
    const containerRef = useRef<HTMLDivElement>(null);

    const scroll = (direction: "left" | "right") => {
        if (containerRef.current) {
            const scrollAmount = direction === "left" ? -300 : 300;
            containerRef.current.scrollBy({
                left: scrollAmount,
                behavior: "smooth",
            });
        }
    };

    return (
        <Box position="relative" width="100%" height="auto">
            <Button
                variant="contained"
                onClick={() => scroll("left")}
                style={{
                    position: "absolute",
                    left: 0,
                    top: "50%",
                    transform: "translateY(-50%)",
                    width: "40px !important",
                    minWidth: "unset",
                    zIndex: 1,
                }}
            >
                ←
            </Button>
            <Button
                variant="contained"
                onClick={() => scroll("right")}
                style={{
                    position: "absolute",
                    right: 0,
                    top: "50%",
                    width: "40px !important",
                    minWidth: "unset",
                    transform: "translateY(-50%)",
                    zIndex: 1,
                }}
            >
                →
            </Button>

            <Box
                ref={containerRef}
                component="div"
                display="flex"
                overflow="hidden"
                whiteSpace="nowrap"
                width="100%"
                padding="10px 40px"
                height="20%"
            >
                {radionice.map((radionica) => (
                    <Radionica radionica={radionica} />
                ))}
            </Box>
        </Box>
    );
}
