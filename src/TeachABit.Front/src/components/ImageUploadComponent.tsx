import { Button } from "@mui/material";
import React, { useState } from "react";
import useImage from "../hooks/useImage";

interface Props {
    setFile: (file: string) => void;
}

export default function ImageUploadComponent(props: Props) {

    const [file, setFile] = useState<File | null>();

    const image = useImage();

    const handleFileChange = async (event: React.ChangeEvent<HTMLInputElement>) => {
        if (event.target.files && event.target.files.length > 0) {
            const f = event.target.files[0];
            setFile(event.target.files[0]);
            props.setFile(await image.toBase64(f));
        }
    }

    return <>
        <form style={{ display: "flex", flexDirection: "column", gap: "10px" }}>
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