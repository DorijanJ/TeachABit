import axios, { InternalAxiosRequestConfig } from "axios";
import globalStore from "../stores/GlobalStore";
import { ApiResponseDto } from "../models/common/ApiResponseDto";
import { MessageResponseDto } from "../models/common/MessageResponseDto";

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
        const config = response.config as RequestInjector;
        if (config.loadingTimeoutId) {
            clearTimeout(config.loadingTimeoutId);
        }
        if (config.loading) {
            globalStore.decrementPageLoading();
        }

        return response.data;
    },
    (error) => {
        const config = error.config as RequestInjector;
        if (config?.loadingTimeoutId) {
            clearTimeout(config.loadingTimeoutId);
        }
        if (config?.loading) {
            globalStore.decrementPageLoading();
        }

        const message: MessageResponseDto | undefined = error.response?.data;

        globalStore.addNotification({
            message: message?.message || "An unexpected error occurred.",
            severity: message?.severity || "error",
        });

        return Promise.reject({
            message: message?.message || "Something went wrong :(",
        });
    }
);

const handleRequest = async <T>(
    promise: Promise<T>
): Promise<T | undefined> => {
    try {
        return await promise;
    } catch (error) {
        console.error("API Error:", error);
        return undefined;
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
