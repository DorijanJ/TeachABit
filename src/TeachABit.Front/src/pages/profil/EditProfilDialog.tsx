import { useState } from "react";

import requests from "../../api/agent";
import ImageUploadComponent from "../../components/ImageUploadComponent";
import { Button } from "@mui/material";

interface Props {
    onClose: () => void;
}

export default function EditProfilDialog(props: Props) {

    const [file, setFile] = useState<File | null>(null);

    const handleSubmit = async () => {
        if (!file) {
            alert("Please select a file to upload.");
            return;
        }

        const formData = new FormData();
        formData.append("profilnaSlika", file);

        const response = await requests.postWithLoading(
            "account/update-korisnik",
            formData
        );
        if (response && response.message?.severity !== "error") {
            props.onClose();
            setFile(null);
        }
    };

    return (
        <>
            <ImageUploadComponent
                setFile={(file: File | null) => setFile(file)}
                file={file} />
            <Button onClick={handleSubmit}>
                {"Spremi"}
            </Button>
        </>
    );
}
