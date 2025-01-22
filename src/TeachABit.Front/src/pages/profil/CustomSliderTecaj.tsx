import { useRef } from "react";
import { TecajDto } from "../../models/TecajDto";
import { Box, Button } from "@mui/material";
import Tecaj from "../tecajevi/Tecaj";

interface Props {
    tecajevi: TecajDto[];
}

export default function CustomSliderTecaj({ tecajevi }: Props) {
    const containerRef = useRef<HTMLDivElement>(null);

    const scroll = (direction: "left" | "right") => {
        if (containerRef.current) {
            const scrollAmount = direction === "left" ? -300 : 300; // Adjust scroll distance
            containerRef.current.scrollBy({
                left: scrollAmount,
                behavior: "smooth",
            });
        }
    };

    return (
        <Box position="relative" width="100%" height={"auto"}>
            <Button
                onClick={() => scroll("left")}
                variant="contained"
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
                onClick={() => scroll("right")}
                variant="contained"
                style={{
                    position: "absolute",
                    right: 0,
                    top: "50%",
                    transform: "translateY(-50%)",
                    width: "40px !important",
                    minWidth: "unset",
                    zIndex: 1,
                }}
            >
                →
            </Button>

            <Box
                ref={containerRef}
                display="flex"
                overflow="hidden"
                whiteSpace="nowrap"
                width="100%"
                padding="10px"
                height="20%"
            >
                {tecajevi.map((tecaj) => (
                    <Tecaj tecaj={tecaj} />
                ))}
            </Box>
        </Box>
    );
}
