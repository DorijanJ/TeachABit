import { test, expect, chromium } from "@playwright/test";
import performLogin from "./helpers/login";
import createDeleteRadionica from "./helpers/createDeleteRadionica";

test("CreateAndDeleteRadionice", async ({ browser }) => {
    const context = await browser.newContext({ ignoreHTTPSErrors: true });
    const page = await context.newPage();

    await performLogin(page);
    await createDeleteRadionica(page);
});