import { IconButton } from "@mui/material";
import { useGlobalContext } from "../../context/Global.context";
import ThumbUpIcon from "@mui/icons-material/ThumbUp";
import ThumbDownIcon from "@mui/icons-material/ThumbDown";

interface Props {
    onLike: () => Promise<any>;
    onDislike: () => Promise<any>;
    onClear: () => Promise<any>;
    likeCount: number | undefined;
    liked?: boolean | undefined;
    size?: "medium" | "small";
}

export default function LikeInfo(props: Props) {
    const globalContext = useGlobalContext();

    return (
        <>
            <div
                style={{
                    display: "flex",
                    flexDirection: "row",
                    alignItems: "center",
                    width: "100%",
                    justifyContent: "flex-end",
                    gap: props.size === "small" ? "4px" : "10px",
                }}
            >
                <IconButton
                    disabled={!globalContext.userIsLoggedIn}
                    onClick={
                        props.liked !== true ? props.onLike : props.onClear
                    }
                >
                    <ThumbUpIcon
                        fontSize={props.size ?? "medium"}
                        color={
                            props.liked === true && globalContext.userIsLoggedIn
                                ? "primary"
                                : "disabled"
                        }
                    ></ThumbUpIcon>
                </IconButton>
                <IconButton
                    disabled={!globalContext.userIsLoggedIn}
                    onClick={
                        props.liked !== false ? props.onDislike : props.onClear
                    }
                >
                    <ThumbDownIcon
                        fontSize={props.size ?? "medium"}
                        color={
                            props.liked === false &&
                            globalContext.userIsLoggedIn
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
}
