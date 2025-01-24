import { Rating } from "@mui/material";
import requests from "../../api/agent";

interface Props {
    tecajId?: number;
    userRating?: number;
}

export default function TecajRating(props: Props) {

    const handleSetRatingValue = (newValue: number | null) => {
        if (newValue !== null) rateTecaj(newValue);
        else deleteRatingTecaj();
    };

    const rateTecaj = async (value: number) => {
        if (value) {
            const response = await requests.postWithLoading(
                `tecajevi/${props.tecajId}/ocjena`,
                {ocjena: value}
            );
            if (response && response.message?.severity === "success") {
                console.log("rated ", value);
            }
        }
    };

    const deleteRatingTecaj = async () => {
        const response = await requests.deleteWithLoading(
            `tecajevi/${props.tecajId}/ocjena`
        );
        if (response && response.message?.severity === "success") {
            console.log("deleted");
        }
    };

    return (
        <>
            {props.tecajId && (
                <Rating
                    value={props.userRating}
                    onChange={(e, newValue) => handleSetRatingValue(newValue)}
                    size="large"
                />
            )}
        </>
    );
}
