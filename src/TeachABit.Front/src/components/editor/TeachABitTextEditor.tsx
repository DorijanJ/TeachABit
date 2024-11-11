import { EditorProvider } from "@tiptap/react";
import { defaultEditorExtensions } from "./DefaultEditorExtensions";
import MenuBar from "./MenuBar";

interface Props {
    content?: string;
    onUpdate: (value: string) => void;
}

export default function TeachABitEditor(props: Props) {
    return (
        <>
            <EditorProvider
                slotBefore={<MenuBar />}
                extensions={defaultEditorExtensions}
                content={props.content}
                onCreate={({ editor }) => {
                    editor.view.dom.setAttribute("spellcheck", "false");
                    editor.view.dom.setAttribute("autocomplete", "off");
                    editor.view.dom.setAttribute("autocapitalize", "off");
                }}
                editorProps={{
                    handleKeyDown(view, event) {
                        if (event.key === "Tab") {
                            event.preventDefault();
                            view.dispatch(view.state.tr.insertText("\t"));
                            return true;
                        }
                        return false;
                    },
                }}
                onUpdate={({ editor }) => {
                    props.onUpdate(editor.getHTML());
                }}
            />
        </>
    );
}
