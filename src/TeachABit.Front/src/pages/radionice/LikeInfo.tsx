import { IconButton } from "@mui/material";
import ThumbUpIcon from "@mui/icons-material/ThumbUp";
import ThumbDownIcon from "@mui/icons-material/ThumbDown";
import { observer } from "mobx-react";
import globalStore from "../../stores/GlobalStore";

interface Props {
    onLike: () => Promise<any>;
    onDislike: () => Promise<any>;
    onClear: () => Promise<any>;
    likeCount: number | undefined;
    liked?: boolean | undefined;
    size?: "medium" | "small";
}

export const LikeInfo = (props: Props) => {
    return (
        <>
            <div
                style={{
                    display: "flex",
                    flexDirection: "row",
                    alignItems: "center",
                    justifyContent: "flex-end",
                    gap: props.size === "small" ? "4px" : "10px",
                }}
            >
                <IconButton
                    disabled={globalStore.currentUser === undefined}
                    onClick={
                        props.liked !== true ? props.onLike : props.onClear
                    }
                >
                    <ThumbUpIcon
                        fontSize={props.size ?? "medium"}
                        color={
                            props.liked === true &&
                            globalStore.currentUser !== undefined
                                ? "primary"
                                : "disabled"
                        }
                    ></ThumbUpIcon>
                </IconButton>
                <IconButton
                    disabled={globalStore.currentUser === undefined}
                    onClick={
                        props.liked !== false ? props.onDislike : props.onClear
                    }
                >
                    <ThumbDownIcon
                        fontSize={props.size ?? "medium"}
                        color={
                            props.liked === false &&
                            globalStore.currentUser !== undefined
                                ? "primary"
                                : "disabled"
                        }
                    ></ThumbDownIcon>
                </IconButton>
                <p style={{ padding: "0 10px", width: "25px" }}>
                    {props.likeCount}
                </p>
            </div>
        </>
    );
};

export default observer(LikeInfo);
