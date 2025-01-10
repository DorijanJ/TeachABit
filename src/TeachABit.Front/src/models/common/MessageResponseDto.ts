export interface MessageResponseDto {
    message: string;
    severity: "info" | "warning" | "error" | "success";
    type: "authentication" | "global" | "hidden";
    messageStatusCode?: number;
    code?: string;
}
