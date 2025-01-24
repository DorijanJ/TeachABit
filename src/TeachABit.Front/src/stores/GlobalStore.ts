import { makeAutoObservable } from "mobx";
import Notification from "../models/common/Notification";
import { v1 } from "uuid";
import { AppUserDto } from "../models/AppUserDto";
import { LevelPristupa } from "../enums/LevelPristupa";
import { KorisnikStatus } from "../enums/KorisnikStatus";

// Ovo je mobx store, koristi se za istu stvar kao i context, ali ima drukčiju sintaksu
class GlobalStore {
    pageIsLoading = 0;
    globalNotifications: Notification[] = [];
    currentUser: AppUserDto | undefined;

    constructor() {
        makeAutoObservable(this);
    }

    incrementPageLoading() {
        this.pageIsLoading++;
    }

    decrementPageLoading() {
        this.pageIsLoading = Math.max(...[0, this.pageIsLoading - 1]);
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

    setCurrentUser(user: AppUserDto | undefined) {
        this.currentUser = user;
    }

    hasPermissions(level: LevelPristupa) {
        if (!this.currentUser) return false;
        return (
            this.currentUser.roles?.find((x) => x.levelPristupa >= level) !==
            undefined
        );
    }

    isMuted() {
        return this.currentUser?.korisnikStatusId === KorisnikStatus.Utisan;
    }
}

const globalStore = new GlobalStore();
export default globalStore;
