export interface MessageResponseDto {
    message: string;
    messageType: {
        type: string;
        severity: string;
    };
    messageStatusCode?: number;
}
