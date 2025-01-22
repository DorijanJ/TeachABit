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
            const scrollAmount = direction === "left" ? -390 : 390;
            containerRef.current.scrollBy({
                left: scrollAmount,
                behavior: "smooth",
            });
        }
    };

    return (
        <Box
            position="relative"
            width="100%"
            height={"auto"}
            minWidth={"380px"}
            display="flex"
            gap="20px"
            alignItems={"center"}
        >
            <Button
                onClick={() => scroll("left")}
                variant="contained"
                style={{
                    width: "40px !important",
                    minWidth: "unset",
                    zIndex: 1,
                }}
            >
                ←
            </Button>
            <Box
                ref={containerRef}
                display="flex"
                overflow="hidden"
                whiteSpace="nowrap"
                width="calc(100% - 40px)"
                gap="20px"
                padding="10px"
            >
                {tecajevi.map((tecaj) => (
                    <Tecaj tecaj={tecaj} />
                ))}
            </Box>
            <Button
                onClick={() => scroll("right")}
                variant="contained"
                style={{
                    width: "40px !important",
                    minWidth: "unset",
                    zIndex: 1,
                }}
            >
                →
            </Button>
        </Box>
    );
}
