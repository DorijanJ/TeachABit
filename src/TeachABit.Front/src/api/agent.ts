import axios, { InternalAxiosRequestConfig } from "axios";

interface RequestInjector extends InternalAxiosRequestConfig {
    loading?: boolean;
    loadingTimeoutId?: ReturnType<typeof setTimeout>;
}

axios.defaults.withCredentials = true;

const requests = {
    get: async (endpoint: string, loading: boolean = false) => {
        const response = await axios.get(
            `${import.meta.env.VITE_REACT_API_URL}${endpoint}`,
            { loading } as RequestInjector
        );
        return response.data;
    },
    post: async (endpoint: string, data: any, loading: boolean = false) => {
        const response = await axios.post(
            `${import.meta.env.VITE_REACT_API_URL}${endpoint}`,
            data,
            { loading } as RequestInjector
        );
        return response.data;
    },
    delete: async (endpoint: string, loading: boolean = false) => {
        const response = await axios.delete(
            `${import.meta.env.VITE_REACT_API_URL}${endpoint}`,
            { loading } as RequestInjector
        );
        return response.data;
    },
    getWithLoading: async (endpoint: string) => {
        return requests.get(endpoint, true);
    },
    postWithLoading: async (endpoint: string, data: any) => {
        return requests.post(endpoint, data, true);
    },
    deleteWithLoading: async (endpoint: string) => {
        return requests.delete(endpoint, true);
    },
};

export default requests;
