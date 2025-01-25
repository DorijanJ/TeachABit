const useImage = () => {
    const toBase64 = async (file: File) => {
        return new Promise<string>((resolve, reject) => {
            const reader = new FileReader();

            reader.onload = () => resolve(reader.result as string);
            reader.onerror = () => reject(reader.error);

            reader.readAsDataURL(file);
        });
    };
    return { toBase64 }
};

export default useImage;
