import { EditorContent, useEditor } from "@tiptap/react";
import { defaultReadOnlyEditorExtensions } from "./DefaultEditorExtensions";
import { useEffect } from "react";

interface Props {
    content: string;
}

export default function TeachABitRenderer(props: Props) {
    useEffect(() => {
        editor?.commands.setContent(props.content);
    }, [props.content]);

    const editor = useEditor({
        extensions: defaultReadOnlyEditorExtensions,
        content: props.content,
        editable: false,
        onCreate: ({ editor }) => {
            editor.view.dom.setAttribute("spellcheck", "false");
            editor.view.dom.setAttribute("autocomplete", "off");
            editor.view.dom.setAttribute("autocapitalize", "off");
        },
    });

    return (
        <div className={"readonly-editor"}>
            <EditorContent editor={editor} />
        </div>
    );
}
