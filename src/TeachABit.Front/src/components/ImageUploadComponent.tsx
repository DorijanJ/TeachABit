import { Button } from "@mui/material";
import React, { useState } from "react";
import useImage from "../hooks/useImage";

interface Props {
    setFile: (file: string) => void;
    width?: string | number;
    height?: string | number;
    ratio?: string;
}

export default function ImageUploadComponent(props: Props) {
    const [file, setFile] = useState<File | null>();

    const image = useImage();

    const handleFileChange = async (
        event: React.ChangeEvent<HTMLInputElement>
    ) => {
        if (event.target.files && event.target.files.length > 0) {
            const f = event.target.files[0];
            if (!f.type.startsWith("image/")) {
                alert("Please upload a valid image file.");
                return;
            }

            setFile(event.target.files[0]);
            props.setFile(await image.toBase64(f));
        }
    };

    return (
        <>
            <form
                style={{
                    display: "flex",
                    flexDirection: "column",
                    gap: "10px",
                    alignItems: "center",
                    maxWidth: "100%",
                }}
            >
                <input
                    accept="image/*"
                    style={{ display: "none" }}
                    id="file-upload"
                    type="file"
                    onChange={handleFileChange}
                />
                <label htmlFor="file-upload">
                    <Button variant="outlined" component="span" color="primary">
                        Odabir slike
                    </Button>
                </label>
                {file && (
                    <div>{(file.size / 1024 / 1024).toPrecision(3)} mb</div>
                )}
                {file && (
                    <img
                        src={URL.createObjectURL(file)}
                        style={{
                            width: props.width ?? "80%",
                            height: props.height ?? "auto",
                            objectFit: "cover",
                            aspectRatio: props.ratio ?? "auto",
                        }}
                    />
                )}
            </form>
        </>
    );
}
