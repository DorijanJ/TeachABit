import { useState } from "react";

import requests from "../../api/agent";
import ImageUploadComponent from "../../components/ImageUploadComponent";
import { Button, Dialog, TextField } from "@mui/material";

interface Props {
    onClose: () => void;
    username: string;
}

export default function EditProfilDialog(props: Props) {
    const [file, setFile] = useState<string>("");

    const handleSubmit = async () => {
        const response = await requests.postWithLoading(
            "account/update-korisnik",
            { profilnaSlikaBase64: file, username: newUsername }
        );
        if (response && response.message?.severity !== "error") {
            props.onClose();
            setFile("");
        }
    };

    const [newUsername, setNewUsername] = useState<string>(props.username);

    return (
        <>
            <Dialog open onClose={props.onClose}>
                <div
                    style={{
                        width: "500px",
                        display: "flex",
                        flexDirection: "column",
                        gap: "20px",
                        padding: "20px",
                        alignItems: "center",
                    }}
                >
                    <TextField
                        value={newUsername}
                        onChange={(e) => setNewUsername(e.target.value)}
                    />
                    <ImageUploadComponent
                        setFile={(file: string) => setFile(file)}
                    />
                    <Button
                        variant="contained"
                        disabled={file === "" && newUsername === props.username}
                        onClick={handleSubmit}
                    >
                        {"Spremi"}
                    </Button>
                </div>
            </Dialog>
        </>
    );
}
