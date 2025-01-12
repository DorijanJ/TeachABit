import { test, expect, chromium } from "@playwright/test";
import performLogin from "./helpers/login";
import createDeleteObjava from "./helpers/createDeleteObjava";

test("CreateAndDeleteObjava", async ({ browser }) => {
    const context = await browser.newContext({ ignoreHTTPSErrors: true });
    const page = await context.newPage();

    await performLogin(page);
    await createDeleteObjava(page);
});
