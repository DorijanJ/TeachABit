import { useState } from "react";
import EditIcon from "@mui/icons-material/Edit";
import { Alert, Button, Dialog, IconButton } from "@mui/material";
import requests from "../../api/agent";
import localStyles from "./EditProfilDialog.module.css";
import { MessageResponseDto } from "../../models/common/MessageResponseDto";

interface Props {
    onClose: () => void;
}

export default function EditProfilDialog(props: Props) {
    const [editDialogOpen, setEditDialogOpen] = useState(false);

    const [file, setFile] = useState<File | null>(null);
    const [message, setMessage] = useState<MessageResponseDto>();

    const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        if (event.target.files && event.target.files.length > 0) {
            setFile(event.target.files[0]);
        }
    };

    const handleSubmit = async (event: React.FormEvent) => {
        event.preventDefault();

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
        if (response && response.message?.severity === "error")
            setMessage(response.message);
        else {
            props.onClose();
            setFile(null);
            setEditDialogOpen(false);
        }
    };

    return (
        <>
            <IconButton onClick={() => setEditDialogOpen(true)}>
                <EditIcon />
            </IconButton>

            <Dialog
                maxWidth={false}
                open={editDialogOpen}
                onClose={() => {
                    setEditDialogOpen(false);
                    setFile(null);
                }}
            >
                <form className={localStyles.imageForm} onSubmit={handleSubmit}>
                    <input
                        accept="image/*"
                        style={{ display: "none" }}
                        id="file-upload"
                        type="file"
                        onChange={handleFileChange}
                    />
                    <label htmlFor="file-upload">
                        <Button
                            variant="outlined"
                            component="span"
                            color="primary"
                        >
                            Odabir slike
                        </Button>
                    </label>
                    <span style={{ marginLeft: "10px" }}>
                        {file ? file.name : "No file chosen"}
                    </span>
                    {file && (
                        <img
                            src={URL.createObjectURL(file)}
                            style={{
                                width: "60%",
                                height: "60%",
                            }}
                        />
                    )}
                    <br />
                    <br />
                    <Button
                        disabled={!file}
                        type="submit"
                        variant="contained"
                        color="primary"
                    >
                        Spremi
                    </Button>
                </form>
                <div
                    style={{
                        display: "flex",
                        justifyContent: "center",
                        alignItems: "center",
                        padding: 10,
                        width: "100%",
                        height: "100px",
                        boxSizing: "border-box",
                    }}
                >
                    {message && (
                        <Alert
                            sx={{ width: "80%" }}
                            severity={message.severity}
                        >
                            {message.message}
                        </Alert>
                    )}
                </div>
            </Dialog>
        </>
    );
}
