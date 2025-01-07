export interface MessageResponseDto {
    message: string;
    severity: "info" | "warning" | "error" | "success";
    messageStatusCode?: number;
    code?: string;
}
