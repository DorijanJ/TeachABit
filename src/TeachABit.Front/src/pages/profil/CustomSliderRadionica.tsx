import { useEffect, useMemo, useRef, useState } from "react";
import { Box, Button } from "@mui/material";
import { RadionicaDto } from "../../models/RadionicaDto";
import Radionica from "../radionice/Radionica";

interface Props {
    radionice: RadionicaDto[];
}

export default function CustomSliderRadionica({ radionice }: Props) {
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
            if (page < radionice.length / itemCount) setPage((prev) => prev + 1);
        }
    };

    const radionicePage = useMemo(() => {
        const skip = (page - 1) * itemCount;
        const take = itemCount;
        return radionice.slice(skip, skip + take)
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
                height="357px"
            >
                {radionicePage.map((radionica) => (
                    <Radionica radionica={radionica} key={"radionica" + radionica.id} />
                ))}
            </Box>
            <div style={{ display: "flex", gap: "20px" }}>
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
                {page}/{Math.ceil(radionice.length / itemCount)}
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
            </div>
        </Box>
    );
}
