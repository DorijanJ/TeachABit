import { useEffect, useMemo, useRef, useState } from "react";
import { TecajDto } from "../../models/TecajDto";
import { Box, Button } from "@mui/material";
import Tecaj from "../tecajevi/Tecaj";

interface Props {
    tecajevi: TecajDto[];
}

export default function CustomSliderTecaj({ tecajevi }: Props) {
    const containerRef = useRef<HTMLDivElement>(null);

    const [itemCount, setItemCount] = useState<number>(0);
    const [page, setPage] = useState(1);

    useEffect(() => {
        if (containerRef.current) setItemCount(Math.floor(containerRef.current.clientWidth / 360));
    }), [containerRef.current]

    const scroll = (direction: "left" | "right") => {
        if (direction === "left") {
            if (page > 1) setPage((prev) => prev - 1);
        }
        else if (direction === "right" && itemCount > 0) {
            if (page < tecajevi.length / itemCount) setPage((prev) => prev + 1);
        }
    };

    const tecajeviPage = useMemo(() => {
        const skip = (page - 1) * itemCount;
        const take = itemCount;
        return tecajevi.slice(skip, skip + take)
    }, [itemCount, page])

    return (
        <Box
            position="relative"
            width="100%"
            height={"auto"}
            minWidth={"360px"}
            display="flex"
            flexDirection={"column"}
            gap="20px"
            alignItems={"center"}
        >
            <Box
                ref={containerRef}
                display="grid"
                minWidth={"360px"}
                gridTemplateColumns={`repeat(${itemCount}, minmax(350px, 1fr))`}
                overflow="hidden"
                width="100%"
                gap="20px"
                padding="10px"
                height={"417px"}
            >
                {tecajeviPage.map((tecaj) => (
                    <Tecaj tecaj={tecaj} key={"tecaj" + tecaj.id} />
                ))}
            </Box>
            <div style={{ display: "flex", gap: "20px" }}>
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
                {page}/{Math.ceil(tecajevi.length / itemCount)}
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
            </div>
        </Box >
    );
}
