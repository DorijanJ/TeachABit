import { test } from "@playwright/test";
import performLogin from "./helpers/login";
import createKomentarTecaj from "./helpers/createKomentarTecaj";
import createTecaj from "./helpers/createTecaj";
test("CreateKomentarTecaj",  async ({ browser }) => {
    const context = await browser.newContext({ ignoreHTTPSErrors: true });
    const page = await context.newPage();
    await performLogin(page);
    //await createTecaj(page);
    await createKomentarTecaj(page);
});