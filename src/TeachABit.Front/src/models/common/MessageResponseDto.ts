export interface MessageResponseDto {
    message: string;
    severity: "info" | "warning" | "error";
    messageStatusCode?: number;
    code?: string;
}
