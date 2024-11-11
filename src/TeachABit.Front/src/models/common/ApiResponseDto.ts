import { MessageResponseDto } from "./MessageResponseDto";

export interface ApiResponseDto {
    data?: any;
    message?: MessageResponseDto;
}
