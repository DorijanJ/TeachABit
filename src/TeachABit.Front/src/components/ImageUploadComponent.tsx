import { Button } from "@mui/material";
import React from "react";

interface Props {
    file: File | null;
    setFile: (file: File | null) => void;
}

export default function ImageUploadComponent({ file, setFile }: Props) {
    const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        if (event.target.files && event.target.files.length > 0) {
            setFile(event.target.files[0]);
        }
    }

    return <>
        <form>
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
        </form>
    </>
}