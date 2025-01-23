import { MessageResponseDto } from "./MessageResponseDto";
import { RefreshUserInfoDto } from "./RefreshUserInfoDto";

export class ApiResponseDto {
    data?: any;
    message?: MessageResponseDto;
    refreshUserInfoDto?: RefreshUserInfoDto;
}
