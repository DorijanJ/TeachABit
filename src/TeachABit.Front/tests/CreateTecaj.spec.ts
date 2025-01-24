import { test } from "@playwright/test";
import performLogin from "./helpers/login";
import createTecaj from "./helpers/createTecaj";

test("CreateTecaj", async ({ browser }) => {
    const context = await browser.newContext({ ignoreHTTPSErrors: true });
    const page = await context.newPage();

    await performLogin(page);
    await createTecaj(page);
});