import { makeAutoObservable } from "mobx";

class GlobalStore {
    pageIsLoading = 0;

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
}

const globalStore = new GlobalStore();
export default globalStore;
