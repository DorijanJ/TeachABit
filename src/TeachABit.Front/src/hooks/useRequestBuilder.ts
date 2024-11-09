const useRequestBuilder = () => {
    const buildRequest = (endpoint: string, params: Record<string, any>) => {
        let url = endpoint;

        const queryString = Object.entries(params)
            .filter(([_, value]) => value !== undefined && value !== null)
            .map(
                ([key, value]) =>
                    `${encodeURIComponent(key)}=${encodeURIComponent(value)}`
            )
            .join("&");

        if (queryString) {
            url += "?" + queryString;
        }

        return url;
    };

    return { buildRequest };
};

export default useRequestBuilder;
