import axios, {
    AxiosError,
    AxiosResponse,
    InternalAxiosRequestConfig,
} from "axios";
import globalStore from "../stores/GlobalStore";
import { ApiResponseDto } from "../models/common/ApiResponseDto";
import { RefreshUserInfoDto } from "../models/common/RefreshUserInfoDto";

const USER_STORAGE_KEYS = {
    USERNAME: "username",
    ID: "id",
    ROLES: "roles",
    STATUS: "status",
};

interface RequestInjector extends InternalAxiosRequestConfig {
    loading?: boolean;
    loadingTimeoutId?: ReturnType<typeof setTimeout>;
}

axios.defaults.withCredentials = true;

const saveUserInfo = (userInfo: RefreshUserInfoDto) => {
    if (userInfo.isAuthenticated) {
        localStorage.setItem(USER_STORAGE_KEYS.USERNAME, userInfo.userName);
        localStorage.setItem(USER_STORAGE_KEYS.ID, userInfo.id);
        localStorage.setItem(
            USER_STORAGE_KEYS.ROLES,
            JSON.stringify(userInfo.roles)
        );
        localStorage.setItem(
            USER_STORAGE_KEYS.STATUS,
            userInfo.korisnikStatusId?.toString() || ""
        );
        globalStore.setCurrentUser({
            id: userInfo.id,
            username: userInfo.userName,
            korisnikStatusId: userInfo.korisnikStatusId,
            roles: userInfo.roles,
        });
    } else {
        clearUserInfo();
    }
};

const clearUserInfo = () => {
    Object.values(USER_STORAGE_KEYS).forEach((key) =>
        localStorage.removeItem(key)
    );
    globalStore.setCurrentUser(undefined);
};

const manageLoadingState = (config?: RequestInjector, decrement = false) => {
    if (config?.loadingTimeoutId) {
        clearTimeout(config.loadingTimeoutId);
    }
    if (config?.loading) {
        decrement
            ? globalStore.decrementPageLoading()
            : globalStore.incrementPageLoading();
    }
};

let isRefreshing = false;

axios.interceptors.response.use(
    (response: AxiosResponse) => {
        const config = response.config as RequestInjector;
        manageLoadingState(config, true);

        const userInfo = response.data.refreshUserInfo;

        if (userInfo) saveUserInfo(userInfo);

        return response.data;
    },
    async (error: AxiosError) => {
        const config = error.config as RequestInjector;
        manageLoadingState(config, true);
        const originalRequest = error.config as InternalAxiosRequestConfig & {
            _retry?: boolean;
        };
        const responseData: ApiResponseDto = error.response
            ?.data as ApiResponseDto;
        const userInfo: RefreshUserInfoDto | undefined =
            responseData?.refreshUserInfoDto;

        if (
            responseData?.message?.code === "reauth" &&
            !originalRequest._retry
        ) {
            originalRequest._retry = true;
            isRefreshing = true;
            await requests.get("account/reauth");
            isRefreshing = false;
            return axios(originalRequest);
        }
        if (userInfo) saveUserInfo(userInfo);

        return Promise.reject({ data: responseData });
    }
);

axios.interceptors.request.use(async (config: RequestInjector) => {
    if (config.loading) {
        config.loadingTimeoutId = setTimeout(() => {
            globalStore.incrementPageLoading();
        }, 200);
    }

    return config;
});

const handleRequest = async <T>(
    promise: Promise<T>
): Promise<T | undefined> => {
    try {
        return await promise;
    } catch (error: any) {
        return error?.data ?? undefined;
    }
};

const requests = {
    get: async (
        endpoint: string,
        loading = false
    ): Promise<ApiResponseDto | undefined> =>
        handleRequest(
            axios.get(`${import.meta.env.VITE_REACT_API_URL}/${endpoint}`, {
                loading,
            } as RequestInjector)
        ),

    post: async (
        endpoint: string,
        data: any,
        loading = false
    ): Promise<ApiResponseDto | undefined> =>
        handleRequest(
            axios.post(
                `${import.meta.env.VITE_REACT_API_URL}/${endpoint}`,
                data,
                {
                    loading,
                } as RequestInjector
            )
        ),

    put: async (
        endpoint: string,
        data: any,
        loading = false
    ): Promise<ApiResponseDto | undefined> =>
        handleRequest(
            axios.put(
                `${import.meta.env.VITE_REACT_API_URL}/${endpoint}`,
                data,
                {
                    loading,
                } as RequestInjector
            )
        ),

    delete: async (
        endpoint: string,
        loading = false
    ): Promise<ApiResponseDto | undefined> =>
        handleRequest(
            axios.delete(`${import.meta.env.VITE_REACT_API_URL}/${endpoint}`, {
                loading,
            } as RequestInjector)
        ),

    getWithLoading: async (endpoint: string) => requests.get(endpoint, true),
    postWithLoading: async (endpoint: string, data?: any) =>
        requests.post(endpoint, data, true),
    putWithLoading: async (endpoint: string, data?: any) =>
        requests.put(endpoint, data, true),
    deleteWithLoading: async (endpoint: string) =>
        requests.delete(endpoint, true),
};

export default requests;
