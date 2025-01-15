import { makeAutoObservable } from "mobx";
import Notification from "../models/common/Notification";
import { v1 } from "uuid";

// Ovo je mobx store, koristi se za istu stvar kao i context, ali ima drukčiju sintaksu
class GlobalStore {
    pageIsLoading = 0;
    globalNotifications: Notification[] = [];

    constructor() {
        makeAutoObservable(this);
    }

    incrementPageLoading() {
        this.pageIsLoading++;
    }

    decrementPageLoading() {
        this.pageIsLoading = Math.max(0, this.pageIsLoading - 1);
    }

    get isPageLoading() {
        return this.pageIsLoading > 0;
    }

    addNotification(notification: Notification) {
        notification.id = v1();
        this.globalNotifications.push(notification);
    }

    clearNotification(id?: string) {
        this.globalNotifications = this.globalNotifications.filter(
            (x) => x.id !== id
        );
    }
}

const globalStore = new GlobalStore();
export default globalStore;
