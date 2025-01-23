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
                variant="contained"
                onClick={() => scroll("left")}
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
                {radionice.map((radionica) => (
                    <Radionica
                        radionica={radionica}
                        key={"radionica" + radionica.id}
                    />
                ))}
            </Box>

            <Button
                variant="contained"
                onClick={() => scroll("right")}
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
