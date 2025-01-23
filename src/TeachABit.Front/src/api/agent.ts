import axios, { InternalAxiosRequestConfig } from "axios";
import globalStore from "../stores/GlobalStore";
import { ApiResponseDto } from "../models/common/ApiResponseDto";
import { RefreshUserInfoDto } from "../models/common/RefreshUserInfoDto";
import useAuth from "../hooks/useAuth";

const USERNAME_KEY = "username";
const ID_KEY = "id";
const ROLES = "roles";

interface RequestInjector extends InternalAxiosRequestConfig {
    loading?: boolean;
    loadingTimeoutId?: ReturnType<typeof setTimeout>;
}

axios.defaults.withCredentials = true;

axios.interceptors.request.use(async (config: RequestInjector) => {
    if (config.loading) {
        config.loadingTimeoutId = setTimeout(() => {
            globalStore.incrementPageLoading();
        }, 200);
    }

    return config;
});

axios.interceptors.response.use(
    (response) => {
        var userInfo: RefreshUserInfoDto = response.data.refreshUserInfo;

        const config = response.config as RequestInjector;
        if (config.loadingTimeoutId) {
            clearTimeout(config.loadingTimeoutId);
        }
        if (config.loading) {
            globalStore.decrementPageLoading();
        }

        const reissuedMessage = response.headers["reissued-message"];

        if (reissuedMessage && !userInfo) {
            const reissuedMessageData = JSON.parse(reissuedMessage);
            userInfo = reissuedMessageData;
        }

        if (userInfo) {
            if (userInfo.isAuthenticated) {
                localStorage.setItem(USERNAME_KEY, userInfo.userName);
                localStorage.setItem(ID_KEY, userInfo.id);
                localStorage.setItem(ROLES, JSON.stringify(userInfo.roles));
                window.location.reload();
            } else {
                localStorage.removeItem(USERNAME_KEY);
                localStorage.removeItem(ID_KEY);
                localStorage.removeItem(ROLES);
                window.location.reload();
            }
        }

        return response.data;
    },
    (error) => {
        const userInfo: RefreshUserInfoDto =
            error.response?.data?.refreshUserInfo;
        const config = error.config as RequestInjector;
        if (config?.loadingTimeoutId) {
            clearTimeout(config.loadingTimeoutId);
        }
        if (config?.loading) {
            globalStore.decrementPageLoading();
        }

        const responseData: ApiResponseDto | undefined = error.response?.data;

        if (
            responseData?.message?.type === "global" ||
            responseData?.message?.type === undefined
        ) {
            globalStore.addNotification({
                message:
                    responseData?.message?.message ??
                    "Nešto je pošlo po krivu.",
                severity: responseData?.message?.severity ?? "error",
            });
        }

        if (userInfo) {
            if (userInfo.isAuthenticated) {
                localStorage.setItem(USERNAME_KEY, userInfo.userName);
                localStorage.setItem(ID_KEY, userInfo.id);
                localStorage.setItem(ROLES, JSON.stringify(userInfo.roles));
                window.location.reload();
            } else {
                localStorage.removeItem(USERNAME_KEY);
                localStorage.removeItem(ID_KEY);
                localStorage.removeItem(ROLES);
                window.location.reload();
            }
        }

        return Promise.reject({
            data: responseData,
        });
    }
);

const handleRequest = async (
    promise: Promise<any>
): Promise<ApiResponseDto | undefined> => {
    try {
        return await promise;
    } catch (error: any) {
        return error?.data ?? undefined;
    }
};

const requests = {
    get: async (
        endpoint: string,
        loading: boolean = false
    ): Promise<ApiResponseDto | undefined> => {
        return handleRequest(
            axios.get(`${import.meta.env.VITE_REACT_API_URL}/${endpoint}`, {
                loading,
            } as RequestInjector)
        );
    },
    post: async (
        endpoint: string,
        data: any,
        loading: boolean = false
    ): Promise<ApiResponseDto | undefined> => {
        return handleRequest(
            axios.post(
                `${import.meta.env.VITE_REACT_API_URL}/${endpoint}`,
                data,
                { loading } as RequestInjector
            )
        );
    },
    delete: async (
        endpoint: string,
        loading: boolean = false
    ): Promise<ApiResponseDto | undefined> => {
        return handleRequest(
            axios.delete(`${import.meta.env.VITE_REACT_API_URL}/${endpoint}`, {
                loading,
            } as RequestInjector)
        );
    },
    put: async (
        endpoint: string,
        data: any,
        loading: boolean = false
    ): Promise<ApiResponseDto | undefined> => {
        return handleRequest(
            axios.put(
                `${import.meta.env.VITE_REACT_API_URL}/${endpoint}`,
                data,
                {
                    loading,
                } as RequestInjector
            )
        );
    },
    getWithLoading: async (endpoint: string) => {
        return requests.get(endpoint, true);
    },
    postWithLoading: async (endpoint: string, data?: any) => {
        return requests.post(endpoint, data, true);
    },
    deleteWithLoading: async (endpoint: string) => {
        return requests.delete(endpoint, true);
    },
    putWithLoading: async (endpoint: string, data?: any) => {
        return requests.put(endpoint, data, true);
    },
};

export default requests;
