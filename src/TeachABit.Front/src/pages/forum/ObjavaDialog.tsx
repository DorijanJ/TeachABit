import {
    Dialog,
    DialogTitle,
    DialogContent,
    TextField,
    DialogActions,
    Button,
    Avatar,
    Typography,
} from "@mui/material";
import { ChangeEvent, Dispatch, SetStateAction, useMemo } from "react";
import { ObjavaDto } from "../../models/ObjavaDto";
import { EditorContent, EditorProvider, useEditor } from "@tiptap/react";
import MenuBar from "../../components/editor/MenuBar";
import { defaultEditorExtensions } from "../../components/editor/DefaultEditorExtensions";
import requests from "../../api/agent";
import StarterKit from "@tiptap/starter-kit";

interface Props {
    objava: ObjavaDto;
    setObjava: Dispatch<SetStateAction<ObjavaDto | undefined>>;
    refreshData: () => Promise<any>;
}

export default function ObjavaDialog(props: Props) {
    const handleStvoriObjavu = async (objava: ObjavaDto) => {
        const response = await requests.postWithLoading("objave", objava);
        if (response.data) {
            props.setObjava(undefined);
            props.refreshData();
        }
    };

    const editor = useEditor({
        extensions: [StarterKit],
        content: props.objava.sadrzaj,
        editable: false,
    });

    const isCreating = useMemo(() => {
        return !props.objava.id;
    }, [props.objava.id]);

    return (
        <>
            {props.objava && (
                <Dialog
                    open={props.objava !== undefined}
                    onClose={() => {
                        props.setObjava(undefined);
                    }}
                    maxWidth={false}
                >
                    <DialogTitle>
                        <div
                            style={{
                                display: "flex",
                                flexDirection: "row",
                                justifyContent: "space-between",
                                alignItems: "flex-start",
                            }}
                        >
                            <div
                                style={{
                                    overflowX: "hidden",
                                    whiteSpace: "normal",
                                    maxWidth: "90%",
                                }}
                            >
                                {isCreating
                                    ? `Nova objava`
                                    : `${props.objava.naziv}`}
                            </div>
                            {props.objava.vlasnikUsername && (
                                <div
                                    style={{
                                        display: "flex",
                                        flexDirection: "row",
                                        gap: "10px",
                                        alignItems: "center",
                                    }}
                                >
                                    <Avatar sx={{ width: 30, height: 30 }}>
                                        {props.objava.vlasnikUsername[0]}{" "}
                                    </Avatar>
                                    <Typography
                                        lineHeight={1}
                                        variant="caption"
                                    >
                                        {props.objava.vlasnikUsername}
                                    </Typography>
                                </div>
                            )}
                        </div>
                    </DialogTitle>
                    <DialogContent
                        sx={{
                            height: isCreating ? 600 : "auto",
                            width: "60vw",
                            display: "flex",
                            flexDirection: "column",
                            gap: "20px",
                            paddingTop: "10px !important",
                        }}
                    >
                        {isCreating && (
                            <TextField
                                fullWidth
                                autoFocus
                                label="Naziv"
                                variant="outlined"
                                value={props.objava.naziv || ""}
                                onChange={(e: ChangeEvent<HTMLInputElement>) =>
                                    props.setObjava((prev: any) => ({
                                        ...prev,
                                        naziv: e.target.value,
                                    }))
                                }
                            />
                        )}
                        {isCreating ? (
                            <EditorProvider
                                slotBefore={<MenuBar />}
                                extensions={defaultEditorExtensions}
                                content={props.objava.sadrzaj}
                                editorProps={{
                                    handleKeyDown(view, event) {
                                        if (event.key === "Tab") {
                                            event.preventDefault();
                                            view.dispatch(
                                                view.state.tr.insertText("\t")
                                            );
                                            return true;
                                        }
                                        return false;
                                    },
                                }}
                                onUpdate={({ editor }) => {
                                    props.setObjava((prev: any) => ({
                                        ...prev,
                                        sadrzaj: editor.getHTML(),
                                    }));
                                }}
                            />
                        ) : (
                            <div className={"readonly-editor"}>
                                <EditorContent editor={editor} />
                            </div>
                        )}
                    </DialogContent>
                    <DialogActions>
                        {!props.objava.id && (
                            <Button
                                variant="contained"
                                onClick={() => handleStvoriObjavu(props.objava)}
                            >
                                Stvori objavu
                            </Button>
                        )}
                    </DialogActions>
                </Dialog>
            )}
        </>
    );
}
