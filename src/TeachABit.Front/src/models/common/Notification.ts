export default interface Notification {
    message: string;
    severity: "info" | "warning" | "error" | "success";
    id?: string;
}
