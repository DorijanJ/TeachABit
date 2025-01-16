import { useState } from "react";

import requests from "../../api/agent";
import ImageUploadComponent from "../../components/ImageUploadComponent";
import { Button } from "@mui/material";

interface Props {
    onClose: () => void;
}

export default function EditProfilDialog(props: Props) {

    const [file, setFile] = useState<string>("");

    const handleSubmit = async () => {

        const response = await requests.postWithLoading(
            "account/update-korisnik",
            { profilnaSlikaBase64: file }
        );
        if (response && response.message?.severity !== "error") {
            props.onClose();
            setFile("");
        }
    };

    return (
        <>
            <ImageUploadComponent
                setFile={(file: string) => setFile(file)} />
            <Button onClick={handleSubmit}>
                {"Spremi"}
            </Button>
        </>
    );
}
