import React, { useRef } from "react";
import { Box, Card, CardContent, Button } from "@mui/material";

export default function CustomSlider() {
    const containerRef = useRef<HTMLDivElement>(null);

    const scroll = (direction: "left" | "right") => {
        if (containerRef.current) {
            const scrollAmount = direction === "left" ? -300 : 300; // Adjust scroll distance
            containerRef.current.scrollBy({ left: scrollAmount, behavior: "smooth" });
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

            {/* Card Container */}
            <Box
                ref={containerRef}
                display="flex" /* Ensures horizontal alignment */
                overflow="auto"
                whiteSpace="nowrap" /* Prevents wrapping */
                scrollBehavior="smooth"
                width="100%"
                padding="10px"
            >
                {Array.from({ length: 10 }).map((_, index) => (
                    <Card
                        key={index}
                        style={{
                            margin: "0 10px", /* Horizontal margin between cards */
                            minWidth: "200px", /* Ensures consistent card width */
                            flexShrink: 0, /* Prevents shrinking of cards */
                        }}
                    >
                        <CardContent>Card {index + 1}</CardContent>
                    </Card>
                ))}
            </Box>
        </Box>
    );
}
